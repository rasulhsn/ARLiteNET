using System;
using System.Data;
using System.Data.Common;

namespace ARLiteNET
{
    public static class AdoDbObjectFactory
    {
        public static IDbCommand CreateCommand(AdoConnectionString connectionString) 
            => GetProvider(connectionString).CreateCommand();

        public static IDbDataAdapter CreateDataAdapter(AdoConnectionString connectionString) 
            => GetProvider(connectionString).CreateDataAdapter();

        public static IDbDataParameter CreateParameter(AdoConnectionString connectionString)
            => GetProvider(connectionString).CreateParameter();

        public static IDbConnection CreateConnection(AdoConnectionString connectionString)
        {
            IDbConnection connection = GetProvider(connectionString).CreateConnection();
            connection.ConnectionString = connectionString.ConnectionString;

            return connection;
        }

        private static DbProviderFactory GetProvider(AdoConnectionString connectionString)
        {
            if (!connectionString.HasProviderName) throw new Exception($"The provider name is empty!");

            return DbProviderFactories.GetFactory(connectionString.ProviderName);
        }
    }
}
