using ARLiteNET.Lib.SQLite.Tests.Data.InMemory;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        public SQLiteObjectTests() 
        {
            SQLiteInMemory.PrepareDB();
        }

        [TestMethod]
        public void GetAll_WhenCalled_ShouldReturnNonNullCollection()
        {
            SQLiteObjectStub adoObject = new SQLiteObjectStub();

            IEnumerable<UserObjectDtoStub> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}