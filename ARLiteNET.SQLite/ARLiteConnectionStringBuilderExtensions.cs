using ARLiteNET.Core;

namespace ARLiteNET.SQLite
{
    public static class ARLiteConnectionStringBuilderExtensions
    {
        public static ARLiteConnectionStringBuilder AddSQLiteProvider(this ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            return connectionStringBuilder.AddProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }

        public static ARLiteConnectionStringBuilder SetSQLite3(this ARLiteConnectionStringBuilder connectionStringBuilder, string sqliteFile)
        {
            return connectionStringBuilder.AddSQLiteProvider()
                                    .ConnectionString($"Data Source={sqliteFile};Version=3;");                     
        }
    }
}
