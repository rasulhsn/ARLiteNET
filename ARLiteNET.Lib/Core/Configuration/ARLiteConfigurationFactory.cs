
namespace ARLiteNET.Lib.Core
{
    public abstract class ARLiteConfigurationFactory
    {
        public AdoConnectionString CreateConnectionString()
        {
            ARLiteConnectionStringBuilder connectionStringBuilder =
                new ARLiteConnectionStringBuilder();

            Configure(connectionStringBuilder);

            return sqliteConnectionStringBuilder.Build();
        }

        protected abstract void Configure(ARLiteConnectionStringBuilder connectionStringBuilder);
    }
}
