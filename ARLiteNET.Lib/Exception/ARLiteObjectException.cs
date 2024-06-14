using System;
using System.Collections.Generic;

namespace ARLiteNET.Lib
{
    [Serializable]
    public class ARLiteObjectException : System.Exception
    {
        public IEnumerable<Exception> InnerExceptions { get; }

        public ARLiteObjectException(string message, IEnumerable<Exception> innerExceptions) : base(message)
        {
            this.InnerExceptions = innerExceptions;
        }
    }
}
