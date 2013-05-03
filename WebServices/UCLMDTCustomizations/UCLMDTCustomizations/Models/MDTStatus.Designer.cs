﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("Models", "StatusLog", "Status", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(UCLMDTCustomizations.Models.Status), "Log", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UCLMDTCustomizations.Models.Log), true)]
[assembly: EdmRelationshipAttribute("Models", "StatusVariable", "Status", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(UCLMDTCustomizations.Models.Status), "Variable", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UCLMDTCustomizations.Models.Variable), true)]

#endregion

namespace UCLMDTCustomizations.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class InventoryEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new InventoryEntities object using the connection string found in the 'InventoryEntities' section of the application configuration file.
        /// </summary>
        public InventoryEntities() : base("name=InventoryEntities", "InventoryEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new InventoryEntities object.
        /// </summary>
        public InventoryEntities(string connectionString) : base(connectionString, "InventoryEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new InventoryEntities object.
        /// </summary>
        public InventoryEntities(EntityConnection connection) : base(connection, "InventoryEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Log> Logs
        {
            get
            {
                if ((_Logs == null))
                {
                    _Logs = base.CreateObjectSet<Log>("Logs");
                }
                return _Logs;
            }
        }
        private ObjectSet<Log> _Logs;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Status> Status
        {
            get
            {
                if ((_Status == null))
                {
                    _Status = base.CreateObjectSet<Status>("Status");
                }
                return _Status;
            }
        }
        private ObjectSet<Status> _Status;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Variable> Variables
        {
            get
            {
                if ((_Variables == null))
                {
                    _Variables = base.CreateObjectSet<Variable>("Variables");
                }
                return _Variables;
            }
        }
        private ObjectSet<Variable> _Variables;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Logs EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToLogs(Log log)
        {
            base.AddObject("Logs", log);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Status EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToStatus(Status status)
        {
            base.AddObject("Status", status);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Variables EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToVariables(Variable variable)
        {
            base.AddObject("Variables", variable);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Models", Name="Log")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Log : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Log object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="file">Initial value of the File property.</param>
        /// <param name="statusId">Initial value of the StatusId property.</param>
        public static Log CreateLog(global::System.Int32 id, global::System.Byte[] file, global::System.Guid statusId)
        {
            Log log = new Log();
            log.Id = id;
            log.File = file;
            log.StatusId = statusId;
            return log;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Byte[] File
        {
            get
            {
                return StructuralObject.GetValidValue(_File);
            }
            set
            {
                OnFileChanging(value);
                ReportPropertyChanging("File");
                _File = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("File");
                OnFileChanged();
            }
        }
        private global::System.Byte[] _File;
        partial void OnFileChanging(global::System.Byte[] value);
        partial void OnFileChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                OnStatusIdChanging(value);
                ReportPropertyChanging("StatusId");
                _StatusId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StatusId");
                OnStatusIdChanged();
            }
        }
        private global::System.Guid _StatusId;
        partial void OnStatusIdChanging(global::System.Guid value);
        partial void OnStatusIdChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Models", "StatusLog", "Status")]
        public Status Status
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusLog", "Status").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusLog", "Status").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Status> StatusReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusLog", "Status");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Status>("Models.StatusLog", "Status", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Models", Name="Status")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Status : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Status object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Status CreateStatus(global::System.Guid id)
        {
            Status status = new Status();
            status.Id = id;
            return status;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Engineer
        {
            get
            {
                return _Engineer;
            }
            set
            {
                OnEngineerChanging(value);
                ReportPropertyChanging("Engineer");
                _Engineer = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Engineer");
                OnEngineerChanged();
            }
        }
        private global::System.String _Engineer;
        partial void OnEngineerChanging(global::System.String value);
        partial void OnEngineerChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Property
        {
            get
            {
                return _Property;
            }
            set
            {
                OnPropertyChanging(value);
                ReportPropertyChanging("Property");
                _Property = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Property");
                OnPropertyChanged();
            }
        }
        private global::System.String _Property;
        partial void OnPropertyChanging(global::System.String value);
        partial void OnPropertyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MachineId
        {
            get
            {
                return _MachineId;
            }
            set
            {
                OnMachineIdChanging(value);
                ReportPropertyChanging("MachineId");
                _MachineId = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MachineId");
                OnMachineIdChanged();
            }
        }
        private global::System.String _MachineId;
        partial void OnMachineIdChanging(global::System.String value);
        partial void OnMachineIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ComputerName
        {
            get
            {
                return _ComputerName;
            }
            set
            {
                OnComputerNameChanging(value);
                ReportPropertyChanging("ComputerName");
                _ComputerName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ComputerName");
                OnComputerNameChanged();
            }
        }
        private global::System.String _ComputerName;
        partial void OnComputerNameChanging(global::System.String value);
        partial void OnComputerNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Severity
        {
            get
            {
                return _Severity;
            }
            set
            {
                OnSeverityChanging(value);
                ReportPropertyChanging("Severity");
                _Severity = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Severity");
                OnSeverityChanged();
            }
        }
        private global::System.String _Severity;
        partial void OnSeverityChanging(global::System.String value);
        partial void OnSeverityChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String StepName
        {
            get
            {
                return _StepName;
            }
            set
            {
                OnStepNameChanging(value);
                ReportPropertyChanging("StepName");
                _StepName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("StepName");
                OnStepNameChanged();
            }
        }
        private global::System.String _StepName;
        partial void OnStepNameChanging(global::System.String value);
        partial void OnStepNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String DartIP
        {
            get
            {
                return _DartIP;
            }
            set
            {
                OnDartIPChanging(value);
                ReportPropertyChanging("DartIP");
                _DartIP = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("DartIP");
                OnDartIPChanged();
            }
        }
        private global::System.String _DartIP;
        partial void OnDartIPChanging(global::System.String value);
        partial void OnDartIPChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String DartPort
        {
            get
            {
                return _DartPort;
            }
            set
            {
                OnDartPortChanging(value);
                ReportPropertyChanging("DartPort");
                _DartPort = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("DartPort");
                OnDartPortChanged();
            }
        }
        private global::System.String _DartPort;
        partial void OnDartPortChanging(global::System.String value);
        partial void OnDartPortChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String DartTicket
        {
            get
            {
                return _DartTicket;
            }
            set
            {
                OnDartTicketChanging(value);
                ReportPropertyChanging("DartTicket");
                _DartTicket = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("DartTicket");
                OnDartTicketChanged();
            }
        }
        private global::System.String _DartTicket;
        partial void OnDartTicketChanging(global::System.String value);
        partial void OnDartTicketChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CurrentStep
        {
            get
            {
                return _CurrentStep;
            }
            set
            {
                OnCurrentStepChanging(value);
                ReportPropertyChanging("CurrentStep");
                _CurrentStep = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CurrentStep");
                OnCurrentStepChanged();
            }
        }
        private global::System.String _CurrentStep;
        partial void OnCurrentStepChanging(global::System.String value);
        partial void OnCurrentStepChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String TotalSteps
        {
            get
            {
                return _TotalSteps;
            }
            set
            {
                OnTotalStepsChanging(value);
                ReportPropertyChanging("TotalSteps");
                _TotalSteps = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("TotalSteps");
                OnTotalStepsChanged();
            }
        }
        private global::System.String _TotalSteps;
        partial void OnTotalStepsChanging(global::System.String value);
        partial void OnTotalStepsChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String SmsUniqueID
        {
            get
            {
                return _SmsUniqueID;
            }
            set
            {
                OnSmsUniqueIDChanging(value);
                ReportPropertyChanging("SmsUniqueID");
                _SmsUniqueID = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("SmsUniqueID");
                OnSmsUniqueIDChanged();
            }
        }
        private global::System.String _SmsUniqueID;
        partial void OnSmsUniqueIDChanging(global::System.String value);
        partial void OnSmsUniqueIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String MessageID
        {
            get
            {
                return _MessageID;
            }
            set
            {
                OnMessageIDChanging(value);
                ReportPropertyChanging("MessageID");
                _MessageID = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("MessageID");
                OnMessageIDChanged();
            }
        }
        private global::System.String _MessageID;
        partial void OnMessageIDChanging(global::System.String value);
        partial void OnMessageIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String VmHost
        {
            get
            {
                return _VmHost;
            }
            set
            {
                OnVmHostChanging(value);
                ReportPropertyChanging("VmHost");
                _VmHost = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("VmHost");
                OnVmHostChanged();
            }
        }
        private global::System.String _VmHost;
        partial void OnVmHostChanging(global::System.String value);
        partial void OnVmHostChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String VmName
        {
            get
            {
                return _VmName;
            }
            set
            {
                OnVmNameChanging(value);
                ReportPropertyChanging("VmName");
                _VmName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("VmName");
                OnVmNameChanged();
            }
        }
        private global::System.String _VmName;
        partial void OnVmNameChanging(global::System.String value);
        partial void OnVmNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Custodian
        {
            get
            {
                return _Custodian;
            }
            set
            {
                OnCustodianChanging(value);
                ReportPropertyChanging("Custodian");
                _Custodian = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Custodian");
                OnCustodianChanged();
            }
        }
        private global::System.String _Custodian;
        partial void OnCustodianChanging(global::System.String value);
        partial void OnCustodianChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Room
        {
            get
            {
                return _Room;
            }
            set
            {
                OnRoomChanging(value);
                ReportPropertyChanging("Room");
                _Room = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Room");
                OnRoomChanged();
            }
        }
        private global::System.String _Room;
        partial void OnRoomChanging(global::System.String value);
        partial void OnRoomChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Location
        {
            get
            {
                return _Location;
            }
            set
            {
                OnLocationChanging(value);
                ReportPropertyChanging("Location");
                _Location = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Location");
                OnLocationChanged();
            }
        }
        private global::System.String _Location;
        partial void OnLocationChanging(global::System.String value);
        partial void OnLocationChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                OnStartTimeChanging(value);
                ReportPropertyChanging("StartTime");
                _StartTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StartTime");
                OnStartTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _StartTime;
        partial void OnStartTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnStartTimeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> Timestamp
        {
            get
            {
                return _Timestamp;
            }
            set
            {
                OnTimestampChanging(value);
                ReportPropertyChanging("Timestamp");
                _Timestamp = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Timestamp");
                OnTimestampChanged();
            }
        }
        private Nullable<global::System.DateTime> _Timestamp;
        partial void OnTimestampChanging(Nullable<global::System.DateTime> value);
        partial void OnTimestampChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Models", "StatusLog", "Log")]
        public EntityCollection<Log> Logs
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Log>("Models.StatusLog", "Log");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Log>("Models.StatusLog", "Log", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Models", "StatusVariable", "Variable")]
        public EntityCollection<Variable> Variables
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Variable>("Models.StatusVariable", "Variable");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Variable>("Models.StatusVariable", "Variable", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="Models", Name="Variable")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Variable : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Variable object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="statusId">Initial value of the StatusId property.</param>
        /// <param name="property">Initial value of the Property property.</param>
        public static Variable CreateVariable(global::System.Int32 id, global::System.Guid statusId, global::System.String property)
        {
            Variable variable = new Variable();
            variable.Id = id;
            variable.StatusId = statusId;
            variable.Property = property;
            return variable;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Guid StatusId
        {
            get
            {
                return _StatusId;
            }
            set
            {
                OnStatusIdChanging(value);
                ReportPropertyChanging("StatusId");
                _StatusId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("StatusId");
                OnStatusIdChanged();
            }
        }
        private global::System.Guid _StatusId;
        partial void OnStatusIdChanging(global::System.Guid value);
        partial void OnStatusIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Property
        {
            get
            {
                return _Property;
            }
            set
            {
                OnPropertyChanging(value);
                ReportPropertyChanging("Property");
                _Property = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Property");
                OnPropertyChanged();
            }
        }
        private global::System.String _Property;
        partial void OnPropertyChanging(global::System.String value);
        partial void OnPropertyChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                OnValueChanging(value);
                ReportPropertyChanging("Value");
                _Value = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Value");
                OnValueChanged();
            }
        }
        private global::System.String _Value;
        partial void OnValueChanging(global::System.String value);
        partial void OnValueChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Models", "StatusVariable", "Status")]
        public Status Status
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusVariable", "Status").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusVariable", "Status").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Status> StatusReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Status>("Models.StatusVariable", "Status");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Status>("Models.StatusVariable", "Status", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}
