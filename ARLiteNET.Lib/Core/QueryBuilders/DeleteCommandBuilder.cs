using System.Data;

namespace ARLiteNET.Lib.Core
{
    public class DeleteCommandBuilder<T> : IDbCommandBuilder
    {
        IDbCommand IDbCommandBuilder.Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
