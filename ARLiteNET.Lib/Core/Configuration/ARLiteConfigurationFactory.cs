
namespace ARLiteNET.Lib
{
    public abstract class ARLiteConfigurationFactory
    {
        public AdoConnectionString CreateConnectionString()
        {
            ARLiteConnectionStringBuilder connectionStringBuilder =
                new ARLiteConnectionStringBuilder();

            Configure(connectionStringBuilder);

            return connectionStringBuilder.Build();
        }

        protected abstract void Configure(ARLiteConnectionStringBuilder connectionStringBuilder);
    }
}
