using System;

namespace ARLiteNET.Lib.Exceptions
{
    [Serializable]
    public class ARLiteException : Exception
    {
        public string ExceptionTypeName { get; }
        public System.Exception InnerException { get; }

        public ARLiteException(string exceptionTypeName,
            System.Exception innerException) : base()
        {
            this.ExceptionTypeName = exceptionTypeName;
            this.InnerException = innerException;
        }
    }
}
