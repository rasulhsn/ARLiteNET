using ARLiteNET.Integration.Tests.Data;
using ARLiteNET.Integration.Tests.Stub;

namespace ARLiteNET.Integration.Tests
{
    [TestClass]
    public class SQLiteRawTests
    {
        [TestInitialize]
        public void Initialize()
        {
            SQLiteInMemory.PrepareDBForTest();
        }

        [TestMethod]
        public void GetAll_WhenCalled_ShouldReturnNonNullCollection()
        {
            SQLiteRawStub adoObject = new SQLiteRawStub();

            IEnumerable<UserDtoStub> objects = adoObject.GetAll();

            Assert.IsNotNull(objects);
        }
    }
}