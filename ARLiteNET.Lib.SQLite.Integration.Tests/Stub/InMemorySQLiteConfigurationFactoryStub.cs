using ARLiteNET.Lib.SQLite;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public sealed class InMemorySQLiteConfigurationFactoryStub : ARLiteConfigurationFactory
    {
        protected override void Configure(ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            connectionStringBuilder.AddSQLiteProvider()
                                    .ConnectionString(Data.SQLiteSettings.ConnectionStringVersion3);                                 
        }
    }
}
