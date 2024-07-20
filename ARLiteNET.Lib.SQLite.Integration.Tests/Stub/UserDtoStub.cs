using ARLiteNET.Lib.Integration.Tests.Data;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public class UserDtoStub : SQLiteDefaultTableObject
    {
        public DateTime? BirthDate { get; set; }
    }
}
