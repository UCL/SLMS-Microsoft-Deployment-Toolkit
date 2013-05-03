using System;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.SQLite;
using System.DirectoryServices;
using System.IO;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web;
using System.Xml.XPath;
using NLog;
using RabbitMQ.Client;
using System.Json;
using System.Data.Objects;
using System.Linq;
using System.Text.RegularExpressions;

namespace UCLMDTCustomizations.Controllers.ComputerService
{

	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
	public class ComputerServiceImpl : IComputersService
	{

		private XPathNavigator configuration;
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private String domainController;


		// Declaring the ReaderWriterLock at the class level
		// makes it visible to all threads.
		static ReaderWriterLock rwl = new ReaderWriterLock();

		public ComputerServiceImpl()
		{
			// Load XML configuration - same one used by PowerShell scripts
			configuration = new XPathDocument(Properties.Settings.Default.Configuration).CreateNavigator();
			domainController = GetConfigSetting(configuration, "/Config/Domain/DomainName");

		}

		/// <summary>
		/// Returns the value of a configuration element from the UCL SLMS AD PowerShell config file based on a XPath query.
		/// Will not work for arrays.
		/// </summary>
		/// <param name="configuration">An XPathNavigator object containing the loaded XML configuration file</param>
		/// <param name="path">XPath on which to perform a query</param>
		/// <returns>String value of the element being queried for</returns>
		private String GetConfigSetting(XPathNavigator configuration, String path)
		{
			XPathExpression expr;
			expr = configuration.Compile(path);
			XPathNodeIterator iterator = configuration.Select(expr);

			if (iterator.MoveNext())
			{
				return iterator.Current.Clone().Value;
			}
			return null;
		}

		public String FormAddGetComputer(Stream input)
		{
			StreamReader sr = new StreamReader(input);
			String s = sr.ReadToEnd();
			sr.Dispose();
			NameValueCollection qs = HttpUtility.ParseQueryString(s);
			return AddGetComputer(qs["school"], qs["smbiosUUID"]);
		}

		/// <summary>
		/// Returns a computer name depending on the SMBIOS UUID. If a computer with the UUID is already in existence,
		/// then return that computer.
		/// </summary>
		/// <param name="school">Which school the computer is to become part of, as per the UCL SLMS AD Configuration file.</param>
		/// <param name="smbiosUUID">The SMBIOS UUID of the computer retrieved through WMI.</param>
		/// <returns>String representing the computer name.</returns>
		public String AddGetComputer(String school, String smbiosUUID)
		{
			// Read configuration data from the UCL SLMS AD Configuration file.

			// Get the device name prefix for clients.
			String deviceNamePrefix = GetConfigSetting(configuration, "/Config/Domain/DeviceTypes/DeviceType[DeviceTypeName=\"Client\"]/NamePrefix");

			// Get the new client devices DN prefix.
			String deviceJoinPrefix = GetConfigSetting(configuration, "/Config/Domain/DeviceTypes/DeviceType[DeviceTypeName=\"Client\"]/OUPath/Prefix");

			// Get the DN suffix for client device objects.
			String deviceSuffix = GetConfigSetting(configuration, "/Config/Domain/DeviceTypes/DeviceType[DeviceTypeName=\"Client\"]/OUPath/Suffix");

			// Get the prefix for the school.
			String schoolPrefix = GetConfigSetting(configuration, "/Config/Domain/Schools/School[SchoolName=\"" + school + "\"]/Prefix");

			// Build the join full OU path for new devices.
			String ouJoinPath = deviceJoinPrefix + schoolPrefix + "," + deviceSuffix;

			// Build the OU path for all current devices.
			String ouPath = "OU=" + schoolPrefix + "," + deviceSuffix;

			// Get the device numeric range for computer names.
			Int32 minimum = Convert.ToInt32(GetConfigSetting(configuration, "/Config/Domain/Schools/School[SchoolName=\"" + school + "\"]/Range/Minimum"));
			Int32 maximum = Convert.ToInt32(GetConfigSetting(configuration, "/Config/Domain/Schools/School[SchoolName=\"" + school + "\"]/Range/Maximum"));
			System.Security.Principal.IIdentity WinId = HttpContext.Current.User.Identity;
			WindowsIdentity wi = (WindowsIdentity)WinId;
			bool existingComputer = false;
			String computerName = "";
			DirectoryEntry entry;
			// Convert the SMBIOS uuid into a format understandable by LDAP.
			Guid guid = new Guid(smbiosUUID);
			WindowsImpersonationContext wic = wi.Impersonate();
			try
			{

				// Connect to AD
				logger.Trace("Searching in " + ouPath);
				entry = new DirectoryEntry("LDAP://" + ouPath);


				String ldapQuery = String.Format("(&(objectCategory=computer)(netbootGUID={0}))", BuildFilterOctetString(guid.ToByteArray()));

				// Test for existing computer.
				logger.Trace("Searching AD for " + ldapQuery);
				DirectorySearcher search = new DirectorySearcher(entry, ldapQuery);
				search.SearchScope = SearchScope.Subtree;
				logger.Trace("No. of results is " + search.FindAll().Count);
				SearchResult result = search.FindOne();
				if (result != null)
				{
					computerName = result.Properties["cn"][0].ToString().ToUpper();
					logger.Trace("Computer name assigned is " + computerName);
					existingComputer = true;

				}
			}
			catch (Exception e)
			{
				throw (e);
			}
			finally
			{
				wic.Undo();
			}

			if (existingComputer)
			{
				return computerName;
			}

			// Code path for new computers


			String nextName = null;

			for (Int32 i = InsertIncrement(school); i <= maximum; i = InsertIncrement(school))
			{
				// If the database doesn't have the minimum number of rows, always start from the school's assigned minimum
				// to avoid overwriting someone else's namespace.
				if (i < minimum)
				{
					i = minimum;
				}

				// Search for the current computer name
				String testName = deviceNamePrefix + i.ToString("00000000");
				String computerSearchQuery = "(&(objectCategory=computer)(cn=" + testName + "))";
				logger.Trace("Searching AD for " + computerSearchQuery);
				DirectorySearcher newNameSearch = new DirectorySearcher(entry, computerSearchQuery);
				newNameSearch.SearchScope = SearchScope.Subtree;

				SearchResult resultNext = newNameSearch.FindOne();
				logger.Trace("No. of results is " + newNameSearch.FindAll().Count);
				if (resultNext == null)
				{
					nextName = testName;
					logger.Trace("Computer name assigned is " + nextName);
					break;
				}
			}
			if (nextName == null)
				return null; // All names were taken

			WindowsImpersonationContext wic2 = wi.Impersonate();
			try
			{
				// Prestage new computer ojbect and apply properties.
				DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + ouJoinPath);
				DirectoryEntry newUser = dirEntry.Children.Add("CN=" + nextName, "computer");
				newUser.Properties["samAccountName"].Value = nextName + "$";
				newUser.Properties["netbootGUID"].Value = guid.ToByteArray();
				newUser.Properties["description"].Value = "Auto-joined by SLMSWDS01";
				newUser.Properties["userAccountControl"].Value = 4128;
				newUser.CommitChanges();
				newUser.Close();
			}
			catch (Exception e)
			{
				logger.Error("Unable to add computer: " + e.ToString());
				throw (e);
			}
			finally
			{
				wic2.Undo();
			}
			return nextName;
		}


		public void FormSetComputerDescription(Stream input)
		{
			StreamReader sr = new StreamReader(input);
			String s = sr.ReadToEnd();
			sr.Dispose();
			NameValueCollection qs = HttpUtility.ParseQueryString(s);
			String location;
			if (String.IsNullOrEmpty(qs["location"]))
			{
				location = "";
			}
			else
			{
				location = qs["location"];
			}
			String roomNo;
			if (String.IsNullOrEmpty(qs["roomNo"]))
			{
				roomNo = "";
			}
			else
			{
				roomNo = qs["roomNo"];
			}
			SetComputerDescription(qs["computerName"], qs["owner"], qs["buildingNo"], roomNo, location, qs["machineType"]);
		}

		public void FormSetComputerSerialNo(Stream input)
		{
			StreamReader sr = new StreamReader(input);
			String s = sr.ReadToEnd();
			sr.Dispose();
			NameValueCollection qs = HttpUtility.ParseQueryString(s);
			String make;
			if (String.IsNullOrEmpty(qs["make"]))
			{
				make = "";
			}
			else
			{
				make = qs["make"];
			}
			String model;
			if (String.IsNullOrEmpty(qs["model"]))
			{
				model = "";
			}
			else
			{
				model = qs["model"];
			}
			String serial;
			if (String.IsNullOrEmpty(qs["serial"]))
			{
				serial = "";
			}
			else
			{
				serial = qs["serial"];
			}
			SetComputerSerialNo(qs["computerName"], make, model, serial);
		}

		/// <summary>
		/// Set the computer's description in a concatenated attribute plus individual ones.
		/// </summary>
		/// <param name="computerName">The name of the computer</param>
		/// <param name="owner">The user name of the owner.</param>
		/// <param name="buildingNo">The building number from the UCL Estates Building Register</param>
		/// <param name="location">Extended location information</param>
		/// <param name="machineType">The machine type. One of Desktop, Laptop or Virtual</param>
		public void SetComputerDescription(String computerName, String owner, String buildingNo, String roomNo, String location, String machineType)
		{
			System.Security.Principal.IIdentity WinId = HttpContext.Current.User.Identity;
			WindowsIdentity wi = (WindowsIdentity)WinId;
			WindowsImpersonationContext wic = wi.Impersonate();
			try
			{
				// Get the computer object.
				DirectoryEntry computer = getComputer(computerName);

				// Get the owner's user object.
				String userFilter = String.Format("(&(objectCategory=person)(sAMAccountName={0}))", owner);
				DirectoryEntry ownerObject = getObject(userFilter);

				// Get the Live@UCL display name.
				String userDisplayName = (String)ownerObject.Properties["extensionAttribute15"].Value;

				// Strip the display name of commas and spaces.
				userDisplayName = userDisplayName.Replace(",", "");
				userDisplayName = userDisplayName.Replace(" ", "");

				// Set the managed by attribute to the DN of the user.
				String userDN = (String)ownerObject.Properties["distinguishedName"].Value;
				computer.Properties["managedBy"].Value = userDN;

				// Get today's date and strip the slash.
				String date = DateTime.Today.ToString("d");
				date = date.Replace("/", "");

				// Build the description property via concatenation.
				String description = userDisplayName + "-b" + buildingNo + "-" + machineType + "-" + date;
				computer.Properties["description"].Value = description;
				computer.Properties["houseIdentifier"].Insert(0, buildingNo);
				if (!String.IsNullOrEmpty(roomNo))
				{
					computer.Properties["roomNumber"].Insert(0, roomNo);
				}
				computer.Properties["location"].Value = location + ", " + roomNo + ", b" + buildingNo;
				computer.CommitChanges();
			}
			catch (Exception e)
			{
				logger.Trace(e.Message);
				logger.Trace(e.StackTrace);
				throw (e);
			}
			finally
			{
				wic.Undo();
			}
		}

		/// <summary>
		/// Set the computer's serial number.
		/// </summary>
		/// <param name="computerName">The name of the computer</param>
		/// <param name="make">Manufacturer of the PC</param>
		/// <param name="model">Model of the PC</param>
		/// <param name="serial">Serial number of the PC</param>
		public void SetComputerSerialNo(String computerName, String make, String model, String serial)
		{
			System.Security.Principal.IIdentity WinId = HttpContext.Current.User.Identity;
			WindowsIdentity wi = (WindowsIdentity)WinId;
			WindowsImpersonationContext wic = wi.Impersonate();
			try
			{
				// Get the computer object.
				DirectoryEntry computer = getComputer(computerName);
				String newSerial = make + ";" + model + ";" + serial;
				logger.Trace("Attempting to set serial of " + computer + " to " + newSerial);
				computer.Properties["serialNumber"].Insert(0, make + ";" + model + ";" + serial);
				computer.CommitChanges();
			}
			catch (Exception e)
			{
				logger.Trace(e.Message);
				logger.Trace(e.StackTrace);
				throw (e);
			}
			finally
			{
				wic.Undo();
			}
		}


		public void FormResetComputerOu(Stream input)
		{
			StreamReader sr = new StreamReader(input);
			String s = sr.ReadToEnd();
			sr.Dispose();
			NameValueCollection qs = HttpUtility.ParseQueryString(s);
			resetComputerOu(qs["computerName"], qs["school"]);
		}

		/// <summary>
		///  Moves a computer back to their school's deployment OU
		/// </summary>
		/// <param name="computerName">Name of the computer</param>
		/// <param name="school">Which school</param>
		public void resetComputerOu(String computerName, String school)
		{
			System.Security.Principal.IIdentity WinId = HttpContext.Current.User.Identity;
			WindowsIdentity wi = (WindowsIdentity)WinId;
			WindowsImpersonationContext wic = wi.Impersonate();
			try
			{
				// Get the new client devices DN prefix.
				String deviceJoinPrefix = GetConfigSetting(configuration, "/Config/Domain/DeviceTypes/DeviceType[DeviceTypeName=\"Client\"]/OUPath/Prefix");

				// Get the DN suffix for client device objects.
				String deviceSuffix = GetConfigSetting(configuration, "/Config/Domain/DeviceTypes/DeviceType[DeviceTypeName=\"Client\"]/OUPath/Suffix");

				// Get the prefix for the school.
				String schoolPrefix = GetConfigSetting(configuration, "/Config/Domain/Schools/School[SchoolName=\"" + school + "\"]/Prefix");

				// Build the join full OU path for new devices.
				String ouJoinPath = deviceJoinPrefix + schoolPrefix + "," + deviceSuffix;

				DirectoryEntry computer = getComputer(computerName);
				computer.MoveTo(new DirectoryEntry("LDAP://" + ouJoinPath));
				computer.CommitChanges();
			}
			catch (Exception e)
			{
				throw (e);
			}
			finally
			{
				wic.Undo();
			}
		}



		/// <summary>
		/// Returns the LDAP entry for a known computer.
		/// </summary>
		/// <param name="computerName"></param>
		/// <returns>DirectoryEntry LDAP Object</returns>
		private DirectoryEntry getComputer(String computerName)
		{
			String filter = String.Format("(&(objectCategory=computer)(sAMAccountName={0}$))", computerName);
			return getObject(filter);
		}

		/// <summary>
		///  Gets a known LDAP object
		/// </summary>
		/// <param name="filter">LDAP filter for the known object</param>
		/// <returns>DirectoryEntry LDAP object</returns>
		private DirectoryEntry getObject(String filter)
		{
			DirectoryEntry dirEntry = new DirectoryEntry("LDAP://" + domainController);
			DirectorySearcher dirSearcher = new DirectorySearcher(dirEntry);
			dirSearcher.Filter = String.Format(filter);
			return dirSearcher.FindOne().GetDirectoryEntry();

		}

		/// <summary>
		/// A thread-safe method to return a unique number depending on the school identifier passed. 
		/// This has been implemented as a database query to a per-school table containing the following fields:
		/// Field 1 & Primary-Key: Counter. As Int32 auto-increment, starting from the minimum value assigned by AD governance.
		/// Field 2: Temp. A boolean that is set to true or false. Real value is not important.
		/// 
		/// Why do we use this?
		/// When multiple computers are joining the domain, searching AD to check for existing computers may not be sufficient to avoid
		/// name clashes because LDAP does not support atomicity - especially in the case of object replication across domain controllers.
		/// </summary>
		/// <param name="con">The SQL connection</param>
		/// <param name="school">School identifier as based in UCL SLMS AD configuration.</param>
		/// <returns>Int32 representing increment</returns>
		private static Int32 InsertIncrement(String school)
		{

			Int32 number = 0;

			using (DbConnection con = new SQLiteConnection(Properties.Settings.Default.NumberIncrement))
			{
				con.Open();
				DbDataReader sqlResult;
				// Set the isolation level to lock the table so no inserts are performed below this transaction.
				using (DbTransaction dbTrans = con.BeginTransaction(isolationLevel: System.Data.IsolationLevel.Serializable))
				{
					using (DbCommand cmd = con.CreateCommand())
					{
						cmd.CommandText = "INSERT INTO [" + school + "] (Temp) VALUES ('TRUE')";
						cmd.ExecuteNonQuery();
						cmd.CommandText = "SELECT Counter from[" + school + "]Order by Counter desc Limit 1";
						sqlResult = cmd.ExecuteReader();
					}
					if (sqlResult.HasRows)
					{
						while (sqlResult.Read())
						{
							number = sqlResult.GetInt32(0);
							dbTrans.Commit();
						}
					}
					else
					{
						dbTrans.Commit();
						logger.Error("No valid computer numbers found.");
					}

				}
				return number;
			}
		}

		/// <summary>
		/// Builds an Octet filter String for use in LDAP queries.
		/// </summary>
		/// <param name="bytes">The byte array of the String to be converted into octets.</param>
		/// <returns>An octet filter String representing the byte array.</returns>
		private String BuildFilterOctetString(byte[] bytes)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < bytes.Length; i++)
			{
				sb.Append("\\");
				sb.AppendFormat(
				bytes[i].ToString("X2")
				);

			}
			return sb.ToString();
		}
	}
}
