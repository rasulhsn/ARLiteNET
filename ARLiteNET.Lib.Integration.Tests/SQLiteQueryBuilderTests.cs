using ARLiteNET.Lib.Common;
using ARLiteNET.Lib.Integration.Tests.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ARLiteNET.Lib.Integration.Tests
{
    [TestClass]
    public class SQLiteQueryBuilderTests
    {
        [TestMethod]
        public void Sample()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(TestUserObject)} ";
            SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();

            string generatedQuery = queryBuilder.Select()
                                .From(nameof(TestUserObject))
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample2()
        {
            const string expectedQuery = $"SELECT * FROM {nameof(TestUserObject)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";
            SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();

            string generatedQuery = queryBuilder.Select()
                                .From(nameof(TestUserObject))
                                .Alias("T")
                                .Where(nameof(TestUserObject.Name))
                                .EqualTo("Test")
                                .And(nameof(TestUserObject.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample3()
        {
            const string expectedQuery = $"SELECT T.Id,T.Name FROM {nameof(TestUserObject)} AS T WHERE T.Name = 'Test' AND T.Id > 0 ";
            SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();

            string generatedQuery = queryBuilder.Select("Id","Name")
                                .From(nameof(TestUserObject))
                                .Alias("T")
                                .Where(nameof(TestUserObject.Name))
                                .EqualTo("Test")
                                .And(nameof(TestUserObject.Id))
                                .GreaterThan(0)
                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }

        [TestMethod]
        public void Sample4()
        {
            const string nameRaw = "Rasul"; const string isActiveRaw = "1";
            const string expectedQuery = $"INSERT INTO {nameof(TestUserObject)} ({nameof(TestUserObject.Name)},{nameof(TestUserObject.IsActive)}) VALUES ('{nameRaw}',{isActiveRaw}), ('{nameRaw}',{isActiveRaw})";
            SQLiteQueryBuilder queryBuilder = new SQLiteQueryBuilder();

            string generatedQuery = queryBuilder.Insert(nameof(TestUserObject))
                                                .Value(new InsertValueObject(nameof(TestUserObject.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(TestUserObject.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Value(new InsertValueObject(nameof(TestUserObject.Name), nameRaw, InsertDataType.TEXT)
                                                       ,new InsertValueObject(nameof(TestUserObject.IsActive), isActiveRaw, InsertDataType.INTEGER))
                                                .Build();

            Assert.AreEqual(expectedQuery, generatedQuery);
        }
    }
}
