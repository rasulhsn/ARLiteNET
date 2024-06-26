using ARLiteNET.Lib.Core;
using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Tests.Data.Stub
{
    public sealed class TestInMemorySQLiteConfigurationFactory : ARLiteConfigurationFactory
    {
        const string CONNECTION_STR = "Data Source=InMemoryARLiteNET;Mode=Memory;Cache=Shared;Version=3;";

        protected override void Configure(ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            connectionStringBuilder.AddSQLiteProvider()
                                    .ConnectionString(CONNECTION_STR);                                 
        }
    }
}
