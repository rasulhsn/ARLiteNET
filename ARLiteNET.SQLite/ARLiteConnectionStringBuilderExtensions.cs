
namespace ARLiteNET.SQLite
{
    public static class ARLiteConnectionStringBuilderExtensions
    {
        /// <summary>
        /// Add SQLite provider for ADO.NET access.
        /// </summary>
        public static ARLiteConnectionStringBuilder AddSQLiteProvider(this ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            return connectionStringBuilder.AddProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }

        /// <summary>
        /// Add SQLite version of 3 with provider for ADO.NET access.
        /// </summary>
        public static ARLiteConnectionStringBuilder SetSQLite3(this ARLiteConnectionStringBuilder connectionStringBuilder, string sqliteFile)
        {
            return connectionStringBuilder.AddSQLiteProvider()
                                    .ConnectionString($"Data Source={sqliteFile};Version=3;");                     
        }
    }
}
