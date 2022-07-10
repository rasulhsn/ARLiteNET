namespace ActiveRecordNET.Lib
{
    public abstract class AdoConfigurationFactory
    {
        protected readonly AdoConnectionStringBuilder AdoConnectionStringBuilder;

        public AdoConfigurationFactory()
        {
            AdoConnectionStringBuilder = new AdoConnectionStringBuilder();
        }

        public abstract AdoConnectionString CreateConnectionString();
    }
}
