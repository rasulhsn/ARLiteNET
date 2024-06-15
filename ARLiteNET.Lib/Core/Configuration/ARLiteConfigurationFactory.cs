
namespace ARLiteNET.Lib.Core
{
    public abstract class ARLiteConfigurationFactory
    {
        public AdoConnectionString CreateConnectionString()
        {
            ARLiteConnectionStringBuilder sqliteConnectionStringBuilder =
                new ARLiteConnectionStringBuilder();

            Configure(sqliteConnectionStringBuilder);

            return sqliteConnectionStringBuilder.Build();
        }

        protected abstract void Configure(ARLiteConnectionStringBuilder sqliteConnectionStringBuilder);
    }
}
