using System;

namespace ARLiteNET.Lib.Core
{
    [AttributeUsage(AttributeTargets.Class,
        Inherited = false, AllowMultiple = false)]
    public sealed class ARLiteConfigurationAttribute : Attribute
    {
        private readonly Type _configurationFactory;

        public ARLiteConfigurationAttribute(Type configurationFactory)
        {
            if (configurationFactory == null)
                throw new ArgumentNullException(nameof(configurationFactory));

            _configurationFactory = configurationFactory;
        }

        public ARLiteConfigurationFactory Factory
        {
            get { return Activator.CreateInstance(_configurationFactory) as ARLiteConfigurationFactory; }
        }
    }
}
