using ARLiteNET.Lib.Integration.Tests.Data;

namespace ARLiteNET.Lib.Integration.Tests.Stub
{
    public class UserObjectDtoStub : SQLiteDefaultTableObject
    {
        public DateTime? BirthDate { get; set; }
    }
}
