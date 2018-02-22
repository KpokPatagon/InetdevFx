namespace Inetdev.Multitenancy.DataModels
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Inetdev.Data;
    using Inetdev.Data.Query;

    /// <summary>
    /// <see cref="TenantDataServices"/> implements the data services
    /// for the TenantStore.TENANT table.
    /// </summary>
    public class TenantDataServices : DataServices
    {
        /// <summary>
        /// Creates a new instance of <see cref="TenantDataServices"/>.
        /// </summary>
        public TenantDataServices()
        {
            Schema = "TenantStore";
            SequenceName = null;
        }


        string _alias;
        /// <summary>
        /// The alias for the underlaying table.
        /// </summary>
        public override string Alias
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_alias))
                    _alias = "TENANT";
                return _alias;
            }
            set { _alias = value; }
        }

        string _table;
        /// <summary>
        /// The name of the underlaying table.
        /// </summary>
        public override string Table
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_table))
                    _table = "TENANT";
                return _table;
            }
            set { _table = value; }
        }

        IDictionary<string, Field> _fields;
        /// <summary>
        /// A dictionary of field configuration for the underlaying table.
        /// </summary>
        public override IDictionary<string, Field> Fields
        {
            get 
            {
                if (_fields == null) 
                {
                    _fields = new Dictionary<string, Field>()
                    {
                        ["ID"] = new Field("ID", DbType.Integer, 1, AutomaticType.Numerator),
                        ["NAME"] = new Field("NAME", DbType.Text, 0, AutomaticType.None),
                        ["DISPLAYNAME"] = new Field("DISPLAYNAME", DbType.NText, 0, AutomaticType.None),
                        ["EMAIL"] = new Field("EMAIL", DbType.NText, 0, AutomaticType.None),
                        ["CULTURE"] = new Field("CULTURE", DbType.Text, 0, AutomaticType.None),
                        ["MAILSERVER_ID"] = new Field("MAILSERVER_ID", DbType.Text, 0, AutomaticType.None),
                        ["DBSERVER_ID"] = new Field("DBSERVER_ID", DbType.Text, 0, AutomaticType.None),
                        ["DATABASENAME"] = new Field("DATABASENAME", DbType.Text, 0, AutomaticType.None),
                        ["DATABASEUSER"] = new Field("DATABASEUSER", DbType.Text, 0, AutomaticType.None),
                        ["DATABASEPASS"] = new Field("DATABASEPASS", DbType.Text, 0, AutomaticType.None),
                        ["PSTATE"] = new Field("PSTATE", DbType.Text, 0, AutomaticType.None),
                        ["SSTATE"] = new Field("SSTATE", DbType.Text, 0, AutomaticType.None),
                        ["SSTATECOMMENT"] = new Field("SSTATECOMMENT", DbType.NClob, 0, AutomaticType.None),
                        ["TDURATIONDAYS"] = new Field("TDURATIONDAYS", DbType.Integer, 0, AutomaticType.None),
                        ["TSTARTEDUTC"] = new Field("TSTARTEDUTC", DbType.DateTime, 0, AutomaticType.None),
                        ["TENDSUTC"] = new Field("TENDSUTC", DbType.DateTime, 0, AutomaticType.None)
                    };
                }
                return _fields; 
            }
            set { _fields = value; }
        }


        /// <summary>
        /// Filter tenants.
        /// </summary>
        /// <param name="request">Optional filtering information.</param>
        /// <param name="take">Maximum tenants to return.</param>
        public async Task<IEnumerable<Tenant>> FilterAsync(FilterRequest request, int take)
        {
            EnsureObjectState();

            SelectStatement s;
            s = new SelectStatement(this);

            if (take > 0)
            {
                s.Modifiers.Add(new TopModifier(take));
            }

            if (request != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Term))
                {
                    var flt = request.Term.Contains("%") ? request.Term : string.Format("%{0}%", request.Term);
                    s.WhereConditions.Add(new SearchCondition(
                        new SimplePredicate(new PredicateColumn(Fields["NAME"]), ComparisonOperator.Like, flt, false),
                        new SimplePredicate(RelationalOperator.Or, new PredicateColumn(Fields["DISPLAYNAME"]), ComparisonOperator.Like, flt, false)));
                }

                if (request.Order.IsDefault)
                {
                    s.OrderColumns.Add(new OrderColumn("DISPLAYNAME", SortDirection.Ascending));
                }
                else
                {
                    s.OrderColumns.Add(new OrderColumn(new Column(request.Order.ColumnID), request.Order.Direction));
                }
            }
            else
            {
                s.OrderColumns.Add(new OrderColumn("DISPLAYNAME", SortDirection.Ascending));
            }

            return await BuildModelListAsync<Tenant>(s);
        }

        /// <summary>
        /// Gets a provisioned tenant looking by it's name without considering the case.
        /// </summary>
        /// <param name="name">Tenant name to look for.</param>
        /// <returns>A provisioned tenant or <see langword="null"/> if the tenant does not exists or it is not provisioned.</returns>
        public Tenant GetProvisionedTenantByNameCaseInsensitive(string name)
        {
            EnsureObjectState();

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            SelectStatement s;
            s = new SelectStatement(this);
            s.WhereConditions.Add(new SearchCondition(
                new SimplePredicate(new PredicateColumn(Fields["NAME"]), ComparisonOperator.Equal, name, false),
                new SimplePredicate(RelationalOperator.And,
                    new PredicateColumn(Fields["PSTATE"]), ComparisonOperator.Equal, "Provisioned")));

            return BuildModelList<Tenant>(s).FirstOrDefault();
        }

        /// <summary>
        /// Gets the tenant looking by it's ID.
        /// </summary>
        /// <param name="tenantId">Tenant Id to look for.</param>
        /// <returns>The tenant for the specified Id.</returns>
        public Tenant GetTenantById(int tenantId)
        {
            EnsureObjectState();

            SelectStatement s;
            s = new SelectStatement(this);
            s.WhereConditions.Add(new SearchCondition(
                new SimplePredicate(new PredicateColumn(Fields["ID"]), ComparisonOperator.Equal, tenantId)));

            return BuildModelList<Tenant>(s).FirstOrDefault();
        }

        /// <summary>
        /// Gets the tenant looking by it's name without considering the case.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Tenant GetTenantByNameCaseInsensitive(string name)
        {
            EnsureObjectState();

            SelectStatement s;
            s = new SelectStatement(this);
            s.WhereConditions.Add(new SearchCondition(
                new SimplePredicate(new PredicateColumn(Fields["NAME"]), ComparisonOperator.Equal, name, false)));

            return BuildModelList<Tenant>(s).FirstOrDefault();
        }
    }
}
