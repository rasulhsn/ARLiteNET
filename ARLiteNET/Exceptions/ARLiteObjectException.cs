using System;
using System.Collections.Generic;

namespace ARLiteNET.Exceptions
{
    [Serializable]
    public class ARLiteObjectException : Exception
    {
        public IEnumerable<Exception> InnerExceptions { get; }

        public ARLiteObjectException(string message,
            IEnumerable<Exception> innerExceptions) : base(message) => this.InnerExceptions = innerExceptions;
    }
}
