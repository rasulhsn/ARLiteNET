using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Tests.Data.Stub
{
    public sealed class TestInMemorySQLiteConfigurationFactory : ARLiteConfigurationFactory
    {
        protected override void Configure(ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            connectionStringBuilder.AddSQLiteProvider()
                                    .ConnectionString(SQLite.Tests.Data.InMemory.SQLiteSettings.ConnectionStringVersion3);                                 
        }
    }
}
