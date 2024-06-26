using ARLiteNET.Lib.Core.Mappers;
using ARLiteNET.Lib.Unit.Tests.Stub;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ARLiteNET.Lib.Unit.Tests
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void Map_WhenGivenValidArguments_ShouldReturnValidMappedObject()
        {
            //Assert
            const int expectedCount = 4;
            TestUserObjectDto instance = new ()
            { Id = 1, Name = "Rasul", IsActive = true, BirthDate = DateTime.Now };

            //Act
            MapType mapTypeInstance = Mapper.Map(instance);

            //Arrange
            Assert.IsNotNull(mapTypeInstance);
            Assert.IsTrue(mapTypeInstance.HasMembers);
            Assert.AreEqual(mapTypeInstance.Members.Count(), expectedCount);
        }
    }
}