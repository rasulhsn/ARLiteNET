
using ARLiteNET.SQLite.Integration.Tests.Data;

namespace ARLiteNET.SQLite.Integration.Tests.Stub
{
    public class UserDtoStub : SQLiteDefaultTableObject
    {
        public DateTime? BirthDate { get; set; }
    }
}
