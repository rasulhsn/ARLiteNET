
using ARLiteNET.Integration.Tests.Data;

namespace ARLiteNET.Integration.Tests.Stub
{
    public class UserDtoStub : SQLiteDefaultTableObject
    {
        public DateTime? BirthDate { get; set; }
    }
}
