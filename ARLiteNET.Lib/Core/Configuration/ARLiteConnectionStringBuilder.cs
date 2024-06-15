using System;
using System.Data.Common;

namespace ARLiteNET.Lib.Core
{
    public class ARLiteConnectionStringBuilder
    {
        private string _connectionString;
        private string _providerName;

        public ARLiteConnectionStringBuilder ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public ARLiteConnectionStringBuilder SetProvider(string providerInvariantName, DbProviderFactory factory)
        {
            DbProviderFactories.RegisterFactory(providerInvariantName, factory);
            _providerName = providerInvariantName;
            return this;
        }

        public ARLiteConnectionStringBuilder SetProvider(string providerInvariantName, Type providerFactoryClass)
        {
            DbProviderFactories.RegisterFactory(providerInvariantName, providerFactoryClass);
            _providerName = providerInvariantName;
            return this;
        }

        public AdoConnectionString Build()
        {
            if (string.IsNullOrEmpty(_connectionString)) throw new Exception("Connection string is empty!");
            if (string.IsNullOrEmpty(_providerName)) throw new Exception("Provider name is empty!");

            return new AdoConnectionString(_connectionString, _providerName);
        }
    }
}
