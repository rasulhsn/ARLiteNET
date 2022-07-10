using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveRecordNET
{
    public sealed class AdoConnectionString
    {
        internal AdoConnectionString(string connectionString)
            : this(connectionString, string.Empty)
        {
            ConnectionString = connectionString;
        }

        internal AdoConnectionString(string connectionString, string providerName)
        {
            ConnectionString = connectionString;
            ProviderName = providerName;
        }

        public string ConnectionString
        {
            get;
        }

        public string ProviderName
        {
            get;
        }

        public bool HasProviderName => !string.IsNullOrEmpty(ProviderName);

        public override string ToString()
        {
            return ConnectionString;
        }

        public override bool Equals(object obj)
        {
            AdoConnectionString other = obj as AdoConnectionString;
            if (other == null) return false;

            return other.ConnectionString.Equals(this.ConnectionString) &&
                    other.ProviderName.Equals(this.ProviderName);
        }

        public override int GetHashCode()
        {
            return string.Concat(this.ConnectionString,
                                this.ProviderName).GetHashCode();
        }
    }
}
