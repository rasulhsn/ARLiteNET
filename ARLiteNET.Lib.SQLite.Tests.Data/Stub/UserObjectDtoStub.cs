using ARLiteNET.Lib.SQLite.Tests.Data.InMemory;
using System;

namespace ARLiteNET.Lib.Tests.Data.Stub
{
    public class UserObjectDtoStub : SQLiteDefaultTableObject
    {
        public DateTime? BirthDate { get; set; }
    }
}
