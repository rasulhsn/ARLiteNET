
using ARLiteNET.SQLite.Integration.Tests.Data;
using ARLiteNET.SQLite.Integration.Tests.Stub;

namespace ARLiteNET.SQLite.Integration.Tests
{
    [TestClass]
    public class SQLiteDeclarativeTests
    {
        [TestInitialize]
        public void Initialize() => SQLiteInMemory.PrepareTestDB();

        [TestMethod]
        [Priority(1)]
        public void Add_WhenCorrectInsert_ShouldReturnSuccessOfAddedObject()
        {
            UserDtoStub newUserStub = new()
            {
                Name = "Test",
                IsActive = true,
            };
            const bool expectedResult = true;
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            bool actualResult = adoObject.Add(newUserStub);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [Priority(2)]
        public void GetAll_WhenCorrectSelectSpecified_ShouldReturnSuccessfullyMappedObject()
        {
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            IEnumerable<UserDtoStub> users = adoObject.GetAll();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        [Priority(3)]
        public void GetByName_WhenCorrectWhereClauseWithCondition_ShouldReturnSuccessfullyMappedObject()
        {
            const string name = "Test";
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            IEnumerable<UserDtoStub> users = adoObject.GetByName(name);

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        [Priority(4)]
        public void DeleteByName_WhenCorrectDelete_ShouldReturnSuccessOfDeletedObject()
        {
            const string name = "Test";
            const bool expectedResult = true;
            SQLiteDeclarativeStub adoObject = new SQLiteDeclarativeStub();

            bool actualResult = adoObject.DeleteByName(name);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}