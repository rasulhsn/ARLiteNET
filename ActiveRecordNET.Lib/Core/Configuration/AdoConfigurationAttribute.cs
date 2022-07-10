using System;

namespace ActiveRecordNET.Lib
{
    [AttributeUsage(AttributeTargets.Class,
        Inherited = false, AllowMultiple = false)]
    public sealed class AdoConfigurationAttribute : Attribute
    {
        private readonly Type _configurationFactory;

        public AdoConfigurationAttribute(Type configurationFactory)
        {
            if(configurationFactory == null)
                throw new ArgumentNullException(nameof(configurationFactory));

            _configurationFactory = configurationFactory;
        }

        public AdoConfigurationFactory Factory
        {
            get { return Activator.CreateInstance(_configurationFactory) as AdoConfigurationFactory; }
        }
    }
}
