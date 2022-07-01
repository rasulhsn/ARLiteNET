using System;
using System.Collections.Generic;

namespace ActiveRecordNET
{
    [Serializable]
    public class AdoObjectProxyException : System.Exception
    {
        public IEnumerable<Exception> InnerErrors { get; }
        public AdoObjectProxyException(string message,IEnumerable<Exception> innerErrors ) : base(message)
        {
            this.InnerErrors = innerErrors;
        }
    }
}
