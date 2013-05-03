using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.EnterpriseServices;
using System.IO;

namespace UCLMDTCustomizations.Computers
{
    [DataContract]
    public class Computer
    {
        [DataMember]
        public String ComputerName;

        [DataMember]
        public String smbiosUUID;

        [DataMember]
        public String serialNumber;

        [DataMember]
        public int buildingNumber;

        [DataMember]
        public String manufacturer;

        [DataMember]
        public String model;

        [DataMember]
        public String deskLocation;

        [DataMember]
        public String internalSupplier;

        [DataMember]
        public String roomNumber;

        [DataMember]
        public String purchaseDate;

        [DataMember]
        public String assetTag;

        [DataMember]
        public String machineType;

        [DataMember]
        public String owner;

        [DataMember]
        public String imageType;

        [DataMember]
        public String imageVersion;
    }

    [ServiceContract(Namespace = "http://slms.ucl.ac.uk/services/")]
    public interface IComputersService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/rest/AddGetComputer/{school}/{smbiosUUID}")]
        [Description("Description for GET /template1")]
        String AddGetComputer(String school, String smbiosUUID);

        [OperationContract]
        [WebInvoke(UriTemplate = "/formProcessor/AddGetComputer")]
        String FormAddGetComputer(Stream input);

        [OperationContract]
        [WebInvoke(UriTemplate = "/rest/SetComputerDescription", BodyStyle=WebMessageBodyStyle.Wrapped )]
        [Description("Description for GET /template1")]
        void SetComputerDescription(String computerName, String owner, String buildingNo, String roomNo, String location, String machineType);

        [OperationContract]
        [WebInvoke(UriTemplate = "/formProcessor/SetComputerDescription")]
        void FormSetComputerDescription(Stream input);


        [OperationContract]
        [WebInvoke(UriTemplate = "/rest/resetComputerOu", BodyStyle = WebMessageBodyStyle.Wrapped)]
        [Description("Description for GET /template1")]
        void resetComputerOu(String computerName, String school);

        [OperationContract]
        [WebInvoke(UriTemplate = "/formProcessor/resetComputerOu")]
        void FormResetComputerOu(Stream input);


        [OperationContract]
        [WebInvoke(UriTemplate = "/formProcessor/SetComputerSerialNo")]
        void FormSetComputerSerialNo(Stream input);

        [OperationContract]
        [WebInvoke(UriTemplate = "/formProcessor/MDTLog")]
        void FormMDTLog(Stream input);

        [OperationContract]
        [WebInvoke(UriTemplate = "/rest/SetComputerSerialNo", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SetComputerSerialNo(String computerName, String make, String model, String serial);

        [OperationContract]
        [WebInvoke(UriTemplate = "/MDTMonitorEvent/PostEvent", BodyStyle = WebMessageBodyStyle.Wrapped)]
        String PostEvent(String smsUniqueID, String computerName, String messageID, String severity,
                String stepName, String currentStep, String totalSteps, String id, String message,
                String dartIP, String dartPort, String dartTicket, String vmHost, String vmName, String engineer, String custodian, String room, String location, String timestamp,
            String scriptPath, String component);

        [OperationContract]
        [WebGet(UriTemplate =
"/MDTMonitorEvent/PostEvent?smsUniqueID={smsUniqueID}&computerName={computerName}&messageID={messageID}&severity={severity}&stepName={stepName}&currentStep={currentStep}&totalSteps={totalSteps}&id={id}&message={message}&dartIP={dartIP}&dartPort={dartPort}&dartTicket={dartTicket}&vmHost={vmHost}&VMName={VMName}&engineer={engineer}&custodian={custodian}&room={room}&location={location}&scriptPath={scriptPath}&component={component}&timestamp={timestamp}")]
        String PostEvent2(String smsUniqueID, String computerName, String messageID, String severity,
                String stepName, String currentStep, String totalSteps, String id, String message,
                String dartIP, String dartPort, String dartTicket, String vmHost, String vmName, String engineer, String custodian, String room, String location, String timestamp,
            String scriptPath, String component);


        [OperationContract]
        [WebGet(UriTemplate = "/MDTMonitorEvent/GetSettings?uniqueID={uniqueID}")]
        void GetSettings(String uniqueID);

    }
}
