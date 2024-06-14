using System;

namespace ARLiteNET.Lib.SQLite
{
    [AttributeUsage(AttributeTargets.Class,
        Inherited = false, AllowMultiple = false)]
    public sealed class SQLiteConfigurationAttribute : Attribute
    {
        private readonly Type _configurationFactory;

        public SQLiteConfigurationAttribute(Type configurationFactory)
        {
            if (configurationFactory == null)
                throw new ArgumentNullException(nameof(configurationFactory));

            _configurationFactory = configurationFactory;
        }

        public SQLiteConfigurationFactory Factory
        {
            get { return Activator.CreateInstance(_configurationFactory) as SQLiteConfigurationFactory; }
        }
    }
}
