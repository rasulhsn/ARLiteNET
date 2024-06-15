using System.Data;

namespace ARLiteNET.Lib.Core
{
    public class UpdateCommandBuilder<T> : IDbCommandBuilder
    {
        IDbCommand IDbCommandBuilder.Build()
        {
            throw new System.NotImplementedException();
        }
    }
}
