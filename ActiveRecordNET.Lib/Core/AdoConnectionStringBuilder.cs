using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ActiveRecordNET.Lib
{
    public class AdoConnectionStringBuilder
    {
        private string _connectionString;
        private string _providerName;

        public AdoConnectionStringBuilder ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public AdoConnectionStringBuilder SetProvider(string providerInvariantName, DbProviderFactory factory)
        {
            DbProviderFactories.RegisterFactory(providerInvariantName, factory);
            _providerName = providerInvariantName;
            return this;
        }

        public AdoConnectionStringBuilder SetProvider(string providerInvariantName, Type providerFactoryClass)
        {
            DbProviderFactories.RegisterFactory(providerInvariantName, providerFactoryClass);
            _providerName = providerInvariantName;
            return this;
        }

        public AdoConnectionStringBuilder MSSQL()
        {
            return this.SetProvider("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
        }

        public AdoConnectionStringBuilder SQLLite()
        {
            return this.SetProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }

        public AdoConnectionString Build()
        {
            if (string.IsNullOrEmpty(_connectionString)) throw new Exception("Connection string is empty!");
            if (string.IsNullOrEmpty(_providerName)) throw new Exception("Provider name is empty!");

            return new AdoConnectionString(this._connectionString, this._providerName);
        }
    }
}
