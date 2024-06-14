using ARLiteNET.Lib.Core;

namespace ARLiteNET.Lib.SQLite
{
    public abstract class SQLiteConfigurationFactory
    {
        public AdoConnectionString CreateConnectionString()
        {
            SQLiteConnectionStringBuilder sqliteConnectionStringBuilder =
                new SQLiteConnectionStringBuilder();

            Configure(sqliteConnectionStringBuilder);

            return sqliteConnectionStringBuilder.Build();
        }

        protected abstract void Configure(SQLiteConnectionStringBuilder sqliteConnectionStringBuilder);
    }
}
