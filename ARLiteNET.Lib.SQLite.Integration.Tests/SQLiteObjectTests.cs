using ARLiteNET.Lib.Integration.Tests.Data;
using ARLiteNET.Lib.Integration.Tests.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteObjectTests
    {
        [TestInitialize]
        public void Initialize()
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