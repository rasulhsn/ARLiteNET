using ARLiteNET.Lib.Integration.Tests.Helper;
using ARLiteNET.Lib.SQLite;
using System.IO;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public sealed class TestSQLLiteConfigurationFactory : SQLiteConfigurationFactory
    {
        const string SQL_FILE_NAME = "ARNetDb.db";

        protected override void Configure(SQLiteConnectionStringBuilder sqliteConnectionStringBuilder)
        {
            string pathSqlLite = Path.Combine(PathUtils.TryGetRootPath(), "Data", SQL_FILE_NAME);

            sqliteConnectionStringBuilder.ConnectionString($"Data Source={pathSqlLite};Version=3;");
        }
    }
}
