using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace ActiveRecordNET.Lib
{
    public static class AdoObjectFactory
    {
        public static IDbCommand CreateCommand(AdoConnectionString connectionString)
        {
            return GetProvider(connectionString).CreateCommand();
        }

        public static IDbDataAdapter CreateDataAdapter(AdoConnectionString connectionString)
        {
            return GetProvider(connectionString).CreateDataAdapter();
        }

        public static IDbDataParameter CreateParameter(AdoConnectionString connectionString)
        {
            return GetProvider(connectionString).CreateParameter();
        }

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
