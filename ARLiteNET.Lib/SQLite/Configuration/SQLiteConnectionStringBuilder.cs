using System;
using System.Data.Common;
using ARLiteNET.Lib.Core;

namespace ARLiteNET.Lib.SQLite
{
    public class SQLiteConnectionStringBuilder
    {
        private string _connectionString;
        private string _providerName;

        public SQLiteConnectionStringBuilder ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        private SQLiteConnectionStringBuilder SetProvider(string providerInvariantName, DbProviderFactory factory)
        {
            DbProviderFactories.RegisterFactory(providerInvariantName, factory);
            _providerName = providerInvariantName;
            return this;
        }

        //private SQLiteConnectionStringBuilder SetProvider(string providerInvariantName, Type providerFactoryClass)
        //{
        //    DbProviderFactories.RegisterFactory(providerInvariantName, providerFactoryClass);
        //    _providerName = providerInvariantName;
        //    return this;
        //}

        private SQLiteConnectionStringBuilder SQLite()
        {
            return SetProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }

        public AdoConnectionString Build()
        {
            SQLite();

            if (string.IsNullOrEmpty(_connectionString)) throw new Exception("Connection string is empty!");
            if (string.IsNullOrEmpty(_providerName)) throw new Exception("Provider name is empty!");

            return new AdoConnectionString(_connectionString, _providerName);
        }
    }
}
