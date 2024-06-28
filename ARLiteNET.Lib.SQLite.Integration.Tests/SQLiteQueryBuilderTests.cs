using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.SQLite;
using ARLiteNET.Lib.Tests.Data.Stub;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteQueryBuilderTests
    {
        [TestMethod]
        public void Build_WhenCalledSelectWithAllColumns_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserObjectDtoStub)} ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(UserObjectDtoStub))
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledSelectAndWhereCondition_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(UserObjectDtoStub)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select()
                                .From(nameof(UserObjectDtoStub))
                                .Alias("T")
                                .Where(nameof(UserObjectDtoStub.Name))
                                .EqualTo("Test")
                                .And(nameof(UserObjectDtoStub.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledSelectAndConditionWithSpecifiedColumns_ShouldReturnCorrectQuery()
        {
            const string expectedQuery = $"SELECT T.Id,T.Name FROM {nameof(UserObjectDtoStub)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";

            string generatedQuery = SQLiteQueryFactory.Select("Id","Name")
                                .From(nameof(UserObjectDtoStub))
                                .Alias("T")
                                .Where(nameof(UserObjectDtoStub.Name))
                                .EqualTo("Test")
                                .And(nameof(UserObjectDtoStub.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Build_WhenCalledInsertWithValues_ShouldReturnCorrectQuery()
        {
            const string nameRaw = "Rasul";
            const string isActiveRaw = "1";
            const string expectedQuery = $"INSERT INTO {nameof(UserObjectDtoStub)} ({nameof(UserObjectDtoStub.Name)},{nameof(UserObjectDtoStub.IsActive)}) VALUES ('{nameRaw}',{isActiveRaw}), ('{nameRaw}',{isActiveRaw})";

            string generatedQuery = SQLiteQueryFactory.Insert(nameof(UserObjectDtoStub))
                                                .Value(new InsertValueObject(nameof(UserObjectDtoStub.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(UserObjectDtoStub.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Value(new InsertValueObject(nameof(UserObjectDtoStub.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(UserObjectDtoStub.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }
    }
}
