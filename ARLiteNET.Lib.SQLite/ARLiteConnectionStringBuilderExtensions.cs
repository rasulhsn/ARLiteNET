using ARLiteNET.Lib.Core;

namespace ARLiteNET.Lib.SQLite
{
    public static class ARLiteConnectionStringBuilderExtensions
    {
        public static ARLiteConnectionStringBuilder SQLite(this ARLiteConnectionStringBuilder connectionStringBuilder)
        {
            return connectionStringBuilder.SetProvider("System.Data.SQLite", System.Data.SQLite.SQLiteFactory.Instance);
        }
    }
}
