using ARLiteNET.Lib.Core;

namespace ARLiteNET.Lib.SQLite
{
    public static class ARLiteConnectionStringBuilderExtensions
    {
        public static void SQLite(this ARLiteConnectionStringBuilder connectionStringBuilder, string sqliteFile)
        {
            connectionStringBuilder.ConnectionString($"Data Source={sqliteFile};Version=3;")
                                          .SetProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }
    }
}
