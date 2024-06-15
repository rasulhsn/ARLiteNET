using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.Integration.Tests.Helper;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public sealed class TestSQLiteConfigurationFactory : ARLiteConfigurationFactory
    {
        const string SQL_FILE_NAME = "ARNetDb.db";
        const string FOLDER = "Data";

        protected override void Configure(ARLiteConnectionStringBuilder sqliteConnectionStringBuilder)
        {
            string pathSqlite = Path.Combine(PathUtils.TryGetRootPath(), FOLDER, SQL_FILE_NAME);

            sqliteConnectionStringBuilder.SQLite3(pathSqlite);
        }
    }
}
