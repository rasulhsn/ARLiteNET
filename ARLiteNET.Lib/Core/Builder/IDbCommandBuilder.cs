using System.Data;

namespace ARLiteNET.Lib.Core
{
    public interface IDbCommandBuilder
    {
        public IDbCommand Build();
    }
}
