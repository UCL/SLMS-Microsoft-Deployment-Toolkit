using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.EnterpriseServices;
using System.IO;

namespace UCLMDTCustomizations.Controllers.LogService
{
    [DataContract]
    public class LogEvent
    {
        [DataMember]
        public String UniqueID {get;set;}

        [DataMember]
        public String UUID { get; set; }

        [DataMember]
        public String MacAddresses { get; set; }

        [DataMember]
        public String ComputerName  {get;set;}

        [DataMember]
        public String DartIP { get; set; }

        [DataMember]
        public String DartPort { get; set; }

        [DataMember]
        public String DartTicket { get; set; }

        [DataMember]
        public String VMHost { get; set; }

        [DataMember]
        public String VMName { get; set; }

        [DataMember]
        public String MessageID  {get;set;}

        [DataMember]
        public String Severity { get; set; }


        // Logstash compatibility
        [DataMember]
        public String severity { get; set; }

        [DataMember]
        public String Message { get; set; }

        [DataMember]
        public String StepName  {get;set;}

        [DataMember]
        public String CurrentStep  {get;set;}

        [DataMember]
        public String TotalSteps { get; set; }

        [DataMember]
        public String Operator { get; set; }

        [DataMember]
        public String PrimaryUser { get; set; }

        [DataMember]
        public String Building { get; set; }

        [DataMember]
        public String Room { get; set; }

        [DataMember]
        public String Location { get; set; }

        [DataMember]
        public String Component { get; set; }
       
        [DataMember]
        public String ScriptPath { get; set; }

        // For Logstash / Graylog2 compatibility
        [DataMember]
        public String facility_label { get; set; }

    }

 

    [DataContract]
    public class LogstashMDTEvent
    {
        [DataMember(Name="@message")]
        public String message;

        [DataMember(Name="@source")]
        public String source;

        [DataMember(Name="@source_host")]
        public String source_host;

        [DataMember(Name="@source_path")]
        public String source_path;

        [DataMember(Name="@timestamp")]
        public String timestamp;

        [DataMember(Name="@fields")]
        public LogEvent fields;

    }



    [ServiceContract(Namespace = "http://slms.ucl.ac.uk")]
    public interface ILogService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/LogEvent/PostEvent", ResponseFormat=WebMessageFormat.Xml)]
        String PostEvent(LogEvent logEvent);


    }
}
