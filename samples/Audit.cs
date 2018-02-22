namespace Security
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Inetdev.Data;
    using Inetdev.Context;
    using Inetdev.Multitenancy;
    
    using Jasbis.Security.I18n;

    /// <summary>
    /// Information of a data modification audit.
    /// </summary>
    public partial class Audit : DataModel, IEquatable<Audit>
    {
        /// <summary>
        /// Creates a new instance of <see cref="Audit"/>.
        /// </summary>
        public Audit()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Audit"/>.
        /// </summary>
        /// <param name="id">Audit record Id</param>
        public Audit(string id)
        {
            Id = id;
        }


        string _id;
        /// <summary>
        /// Audit record Id
        /// </summary>
        [TextDataProperty("Id", DbType.Text, MinLength = 0, MaxLength = 50, FieldName = "_id", PrimaryKey = 1, AutomaticType = AutomaticType.Identity, Caption = "AuditIdCaption", ResourceType = typeof(ModelLabels))]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged(_id);
            }
        }

        int _partitionKey;
        /// <summary>
        /// Multi-tenant partition key.
        /// </summary>
        [PartitionKey]
        [IntegerDataProperty("PartitionKey", DbType.Integer, FieldName = "_partitionKey", Mandatory = true, DefaultValue = 0, Caption = "AuditPartitionKeyCaption", ResourceType = typeof(ModelLabels))]
        public int PartitionKey
        {
            get { return _partitionKey; }
            set
            {
                _partitionKey = value;
                NotifyPropertyChanged(_partitionKey);
            }
        }

        int _kind;
        /// <summary>
        /// Audit record kind
        /// </summary>
        [IntegerDataProperty("Kind", DbType.Integer, FieldName = "_kind", Mandatory = true, Caption = "AuditKindCaption", ResourceType = typeof(ModelLabels))]
        public AuditKind Kind
        {
            get { return (AuditKind)_kind; }
            set
            {
                _kind = (int)value;
                NotifyPropertyChanged(_kind);
            }
        }

        DateTime _createdOnUtc;
        /// <summary>
        /// Creation date and time on UTC.
        /// </summary>
        [GenericDataProperty("CreatedOnUtc", DbType.DateTime, FieldName = "_createdOnUtc", Mandatory = true, AutomaticType = AutomaticType.CreationDateTimeUtc, Caption = "AuditCreatedOnUtcCaption", ResourceType = typeof(ModelLabels))]
        public DateTime CreatedOnUtc
        {
            get { return _createdOnUtc; }
            set
            {
                _createdOnUtc = value;
                NotifyPropertyChanged(_createdOnUtc);
            }
        }

        string _userId;
        /// <summary>
        /// User Id
        /// </summary>
        [TextDataProperty("UserId", DbType.NText, MinLength = 0, MaxLength = 128, FieldName = "_userId", Mandatory = true, Caption = "AuditUserIdCaption", ResourceType = typeof(ModelLabels))]
        public string UserId
        {
            get { return _userId; }
            set
            {
                _userId = value;
                NotifyPropertyChanged(_userId);
            }
        }

        string _userName;
        /// <summary>
        /// User name
        /// </summary>
        [TextDataProperty("UserName", DbType.NText, MinLength = 0, MaxLength = 260, FieldName = "_userName", Mandatory = true, Caption = "AuditUserNameCaption", ResourceType = typeof(ModelLabels))]
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                NotifyPropertyChanged(_userName);
            }
        }

        string _userAuthority;
        /// <summary>
        /// User authority.
        /// </summary>
        [TextDataProperty("UserAuthority", DbType.NText, MinLength = 0, MaxLength = 128, FieldName = "_userAuthority", Mandatory = true, Caption = "AuditUserAuthorityCaption", ResourceType = typeof(ModelLabels))]
        public string UserAuthority
        {
            get { return _userAuthority; }
            set
            {
                _userAuthority = value;
                NotifyPropertyChanged(_userAuthority);
            }
        }

        string _sourceId;
        /// <summary>
        /// Id of the object that sources the audit record.
        /// </summary>
        [TextDataProperty("SourceId", DbType.Text, MinLength = 0, MaxLength = 256, FieldName = "_sourceId", Mandatory = true, Caption = "AuditSourceIdCaption", ResourceType = typeof(ModelLabels))]
        public string SourceId
        {
            get { return _sourceId; }
            set
            {
                _sourceId = value;
                NotifyPropertyChanged(_sourceId);
            }
        }

        string _sourceProtocol;
        /// <summary>
        /// Protocol of the object that sources the audit record.
        /// </summary>
        [TextDataProperty("SourceProtocol", DbType.Text, MinLength = 0, MaxLength = 256, FieldName = "_sourceProtocol", Mandatory = true, Caption = "AuditSourceProtocolCaption", ResourceType = typeof(ModelLabels))]
        public string SourceProtocol
        {
            get { return _sourceProtocol; }
            set
            {
                _sourceProtocol = value;
                NotifyPropertyChanged(_sourceProtocol);
            }
        }

        string _previousId;
        /// <summary>
        /// Id of the previous audit record
        /// </summary>
        [TextDataProperty("PreviousId", DbType.Text, MinLength = 0, MaxLength = 50, FieldName = "_previousId", Caption = "AuditPreviousIdCaption", ResourceType = typeof(ModelLabels))]
        public string PreviousId
        {
            get { return _previousId; }
            set
            {
                _previousId = value;
                NotifyPropertyChanged(_previousId);
            }
        }

        string _nextId;
        /// <summary>
        /// Id of the next audit record
        /// </summary>
        [TextDataProperty("NextId", DbType.Text, MinLength = 0, MaxLength = 50, FieldName = "_nextId", Caption = "AuditNextIdCaption", ResourceType = typeof(ModelLabels))]
        public string NextId
        {
            get { return _nextId; }
            set
            {
                _nextId = value;
                NotifyPropertyChanged(_nextId);
            }
        }

        string _securityIncidentId;
        /// <summary>
        /// Security incident Id
        /// </summary>
        [TextDataProperty("SecurityIncidentId", DbType.Text, MinLength = 0, MaxLength = 50, FieldName = "_securityIncidentId", Caption = "AuditSecurityIncidentIdCaption", ResourceType = typeof(ModelLabels))]
        public string SecurityIncidentId
        {
            get { return _securityIncidentId; }
            set
            {
                _securityIncidentId = value;
                NotifyPropertyChanged(_securityIncidentId);
            }
        }

        byte[] _data;
        /// <summary>
        /// Audit record data
        /// </summary>
        [GenericDataProperty("Data", DbType.Blob, FieldName = "_data", Mandatory = true, Caption = "AuditDataCaption", ResourceType = typeof(ModelLabels))]
        public BinaryAttachment Data
        {
            get
            {
                if (_data == null || _data.Length == 0) return null;
                else return new BinaryAttachment(_data);
            }
            set
            {
                if (value == null) _data = null;
                else _data = value.BinaryData;
                NotifyPropertyChanged(_data);
            }
        }

        Audit _previous;
        /// <summary>
        /// Previous <see cref="Audit"/> item for this source.
        /// </summary>
        public Audit Previous
        {
            get
            {
                if (_previous == null)
                {
                    _previous = Audit.Load(id: PreviousId);
                }
                return _previous;
            }
            set { _previous = value; }
        }

        Audit _next;
        /// <summary>
        /// Next <see cref="Audit"/> item for this source.
        /// </summary>
        public Audit Next
        {
            get
            {
                if (_next == null)
                {
                    _next = Audit.Load(id: NextId);
                }
                return _next;
            }
            set { _next = value; }
        }

        SecurityIncident _securityIncident;
        /// <summary>
        /// A <see cref="SecurityIncident" /> associated to this <see cref="Audit" /> item.
        /// </summary>
        public SecurityIncident SecurityIncident
        {
            get
            {
                if (_securityIncident == null)
                {
                    _securityIncident = SecurityIncident.Load(id: SecurityIncidentId);
                }
                return _securityIncident;
            }
            set { _securityIncident = value; }
        }


        /// <summary>
        /// Whether model validation must be enforced.
        /// </summary>
        [Editable(false)]
        public override bool EnforceModelValidation => false;

        
        #region Static loaders

        /// <summary>
        /// Initializes a new instance of <see cref="Audit"/> by loading the data from the underlaying repository.
        /// </summary>
        /// <param name="id">Audit record Id</param>
        public static Audit Load(string id)
        {
            Audit o;
            o = ApplicationContext.Current.GetObject<Audit>(id);
            o = o.DataServices.GetModel(o);
            return o;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Audit"/> by loading the data from the underlaying repository.
        /// </summary>
        /// <param name="id">Audit record Id</param>
        public static Task<Audit> LoadAsync(string id)
        {
            Audit o;
            o = ApplicationContext.Current.GetObject<Audit>(id);
            o = o.DataServices.GetModel(o);
            return Task.FromResult(o);
        }

        #endregion

        #region IEquatable<Audit> implementation.

        /// <summary>
        /// Checks if <paramref name="other"/> is equals to this object.
        /// </summary>
        public bool Equals(Audit other)
        {
            if (other == null) return false;
            else
            {
                return string.Equals(Id, other.Id, StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Checks if <paramref name="obj"/> is equals to this object.
        /// </summary>
        /// <param name="obj">An object to compare with this object.</param>
        public override bool Equals(object obj)
        {
            return Equals(obj as Audit);
        }

        /// <summary>
        /// Not required, implemented to avoid warnings, returns base.GetHasCode().
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
