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
        public void MapInstance_WhenGivenValidArguments_ShouldReturnValidMappedObject()
        {
            const int expectedCount = 4;
            UserDtoStub instance = new ()
            { Id = 1, Name = "Rasul", IsActive = true, BirthDate = DateTime.Now };

            MapType mapTypeInstance = Mapper.MapInstance(instance);

            Assert.IsNotNull(mapTypeInstance);
            Assert.IsTrue(mapTypeInstance.HasMembers);
            Assert.AreEqual(mapTypeInstance.Members.Count(), expectedCount);
        }
    }
}