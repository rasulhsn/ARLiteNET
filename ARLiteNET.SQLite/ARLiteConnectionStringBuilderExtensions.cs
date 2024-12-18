﻿
namespace ARLiteNET.SQLite
{
    public static class ARLiteConnectionStringBuilderExtensions
    {
        const string PROVIDER_NAME_STR = "System.Data.SQLite";

        /// <summary>
        /// Add SQLite provider for ADO.NET access.
        /// </summary>
        public static ARLiteConnectionStringBuilder AddSQLiteProvider(this ARLiteConnectionStringBuilder connectionStringBuilder) 
            => connectionStringBuilder.AddProvider(PROVIDER_NAME_STR, System.Data.SQLite.SQLiteFactory.Instance);

        /// <summary>
        /// Add SQLite version of 3 with provider for ADO.NET access.
        /// </summary>
        public static ARLiteConnectionStringBuilder SetSQLite3(this ARLiteConnectionStringBuilder connectionStringBuilder, string sqliteFile) 
            => connectionStringBuilder.AddSQLiteProvider()
                                        .ConnectionString($"Data Source={sqliteFile};Version=3;");
    }
}
