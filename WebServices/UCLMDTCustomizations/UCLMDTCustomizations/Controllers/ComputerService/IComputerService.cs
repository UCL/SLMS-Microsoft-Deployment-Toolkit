using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.EnterpriseServices;
using System.IO;

namespace UCLMDTCustomizations.Controllers.ComputerService
{

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
        [WebInvoke(UriTemplate = "/rest/SetComputerSerialNo", BodyStyle = WebMessageBodyStyle.Wrapped)]
        void SetComputerSerialNo(String computerName, String make, String model, String serial);



    }
}
