using System.Data;

namespace ARLiteNET
{
    public interface IDbCommandBuilder
    {
        public IDbCommand Build();
    }
}
