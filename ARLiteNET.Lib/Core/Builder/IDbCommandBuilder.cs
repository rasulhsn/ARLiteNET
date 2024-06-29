using System.Data;

namespace ARLiteNET.Lib
{
    public interface IDbCommandBuilder
    {
        public IDbCommand Build();
    }
}
