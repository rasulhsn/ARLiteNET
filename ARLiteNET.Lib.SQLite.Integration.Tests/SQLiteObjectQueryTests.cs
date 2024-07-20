using ARLiteNET.Lib.Integration.Tests.Data;
using ARLiteNET.Lib.Integration.Tests.Stub;
using ARLiteNET.Lib.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.Lib.SQLite.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectQueryTests
    {
        [TestInitialize]
        public void Initialize()
        {
            SQLiteInMemory.PrepareDBForTest();
        }

        [TestMethod]
        public void GetAll_WhenCorrectSelectSpecified_ShouldReturnSuccessfullyMappedObject()
        {
            SQLiteObjectQueryStub adoObject = new SQLiteObjectQueryStub();

            IEnumerable<UserDtoStub> users = adoObject.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }
    }
}
