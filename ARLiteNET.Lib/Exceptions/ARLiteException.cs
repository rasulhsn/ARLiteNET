using System;

namespace ARLiteNET.Lib.Exceptions
{
    [Serializable]
    public class ARLiteException : Exception
    {
        public string MethodName { get; }
        public string ExceptionTypeStr { get; }

        public ARLiteException(string exceptionTypeStr,
                        string methodName,
                        Exception innerException) : base("Occur error in ARLiteNET!", innerException)
        {
            this.ExceptionTypeStr = exceptionTypeStr;
            this.MethodName = methodName;
        }
    }
}
