using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.Integration.Tests.Helper;
using System.IO;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public sealed class TestSQLiteConfigurationFactory : ARLiteConfigurationFactory
    {
        const string SQL_FILE_NAME = "ARNetDb.db";
        const string FOLDER = "Data";

        protected override void Configure(ARLiteConnectionStringBuilder sqliteConnectionStringBuilder)
        {
            string pathSqlLite = Path.Combine(PathUtils.TryGetRootPath(), FOLDER, SQL_FILE_NAME);

            sqliteConnectionStringBuilder.ConnectionString($"Data Source={pathSqlLite};Version=3;")
                                         .SQLite();
        }
    }
}
