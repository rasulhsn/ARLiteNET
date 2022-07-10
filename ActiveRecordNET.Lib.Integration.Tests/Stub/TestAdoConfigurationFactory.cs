using System.IO;
using ActiveRecordNET.Lib.Integration.Tests.Helper;

namespace ActiveRecordNET.Lib.Integration.Tests
{
    public class TestAdoConfigurationFactory : AdoConfigurationFactory
    {
        public override AdoConnectionString CreateConnectionString()
        {
            // For test -> sqllite
            string pathSqlLite = Path.Combine(PathUtils.TryGetRootPath(), "Data", "ARNetDb.db");

            return AdoConnectionStringBuilder.ConnectionString($"Data Source={pathSqlLite};Version=3;")
                   .SQLLite()
                   .Build();
        }
    }
}
