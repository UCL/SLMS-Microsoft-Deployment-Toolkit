using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using RabbitMQ.Client;
using System.Threading;
using AutoMapperAssist;
using AutoMapper;
using System.Json;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.ServiceModel.Channels;

namespace UCLMDTCustomizations.Controllers.LogService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LogServiceImpl : ILogService
    {
        public String PostEvent(LogEvent logEvent)
        {
            
            Models.Entities context = new Models.Entities();
            Guid guid;
            Models.Computer computer;

            try
            {
                guid = Guid.Parse(logEvent.UniqueID);
                computer = (from o in context.Computers where o.UniqueID == guid select o).First();

            }
            catch
            {
                logEvent.UniqueID = null;
            }
            if (String.IsNullOrEmpty(logEvent.UniqueID))
            {
                    guid = Guid.NewGuid();
                    logEvent.UniqueID = guid.ToString();
                    computer = new Models.Computer();
                    computer.UniqueID = guid;
                    computer.StartTime = System.DateTime.Now;
                    context.Computers.AddObject(computer);
            }
            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
            OperationContext httpContext = OperationContext.Current;
            MessageProperties httpProperties = httpContext.IncomingMessageProperties;
            String host = ((RemoteEndpointMessageProperty)(httpProperties[RemoteEndpointMessageProperty.Name])).Address;
            ThreadStart processDelegatedEvent = delegate
            {
                DelegatedEvent(logEvent,host);
            };
            new Thread(processDelegatedEvent).Start();
            return logEvent.UniqueID;
        }

        private void DelegatedEvent(LogEvent logEvent, String host)
        {
            Models.Entities context = new Models.Entities();
            Guid guid;
            Models.Computer computer;
            guid = Guid.Parse(logEvent.UniqueID);
            computer = (from o in context.Computers where o.UniqueID == guid select o).First();
            var logMapper = new Mapper<LogEvent, Models.Computer>();
            logMapper.LoadIntoInstance(logEvent, computer);
            computer.LastTime = System.DateTime.Now;
            context.SaveChanges();

            // Convert SC severity codes to Syslog RFC3164
            switch (logEvent.Severity)
            {
                case "1":
                    logEvent.Severity = "6";
                    break;
                case "2":
                    logEvent.Severity = "4";
                    break;
                case "3":
                    logEvent.Severity = "3";
                    break;
                case "4":
                    logEvent.Severity = "7";
                    break;
            }

            LogstashMDTEvent logstashEvent = new LogstashMDTEvent();
            logEvent.severity = logEvent.Severity;
            logstashEvent.message = logEvent.Message;
            logstashEvent.source = "Microsoft Deployment Toolkit";
            logstashEvent.source_host = host;
            logstashEvent.source_path = logEvent.ScriptPath;
            logstashEvent.timestamp = System.DateTime.Now.ToString("o");
            logEvent.facility_label = "16";
            logstashEvent.fields = logEvent;

            MemoryStream logEventStream = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(LogstashMDTEvent));
            ser.WriteObject(logEventStream, logstashEvent);
            String logstashJSON = Encoding.Default.GetString(logEventStream.ToArray());

            SendMessage(logstashJSON);

            if (!String.IsNullOrEmpty(logEvent.Message))
            {
                if (logEvent.Message.StartsWith("Property"))
                {
                    try { SetProperty(computer.ID, logEvent.Message); }
                    catch { }
                }
            }
        }

        private void SetProperty(int id, String message)
        {
            Models.Property property;
            Match match = Regex.Match(message, @"Property (.*) is now = (.*)$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                Models.Entities context = new Models.Entities();

                String variable = match.Groups[0].Value;
                String value = match.Groups[1].Value;
                try { property = (from o in context.Properties where (o.ComputerID == id && o.PropertyKey == variable) select o).First(); }
                catch
                {
                    property = new Models.Property();
                    property.ComputerID = id;
                    context.Properties.AddObject(property);
                }
                property.PropertyValue = value;
                context.SaveChanges();
            }
            
        }

        private static void SendMessage(String message)
        {
           IModel channel = UCLMDTCustomizations.Utils.SingletonRabbit.channelInstance;
           IBasicProperties basicProperties = channel.CreateBasicProperties();
           channel.BasicPublish(Properties.Settings.Default.AMQPExchange, "logstash", false, false, basicProperties, Encoding.UTF8.GetBytes(message));
        }

    }
}
