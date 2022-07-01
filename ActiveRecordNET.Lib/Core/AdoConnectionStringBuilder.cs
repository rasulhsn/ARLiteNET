using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ActiveRecordNET
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

        public AdoConnectionStringBuilder ProviderName(string providerName)
        {
            _providerName = providerName;
            return this;
        }

        public AdoConnectionStringBuilder MSSQL()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
            return this.ProviderName("System.Data.SqlClient");
        }

        public AdoConnectionStringBuilder SQLLite()
        {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
            return this.ProviderName("System.Data.SQLite");
        }

        public AdoCommandBuilder CreateCommand()
        {
            if (string.IsNullOrEmpty(_connectionString)) throw new Exception("Connection string is empty!");
            if (string.IsNullOrEmpty(_providerName)) throw new Exception("Provider name is empty!");

            return new AdoCommandBuilder(new AdoConnectionString(this._connectionString, this._providerName));
        }
    }
}
