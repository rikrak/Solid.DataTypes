using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.FilePathTests
{
    [TestClass]
    public class ExistsTests : TestBase
    {
        [TestMethod]
        public void DoesNotExist()
        {
            // arrange
            const string path = "S:\\Does\\Not\\Exist.fsd";
            const bool expected = false;
            var target = new FilePath((path));

            // act
            var actual = target.Exists;

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void DoesExist()
        {
            // arrange
            string path = this.FileManager.GetTemporaryFile();
            const bool expected = true;
            var target = new FilePath((path));

            // act
            var actual = target.Exists;

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void UndefinedTest()
        {
            // arrange
            var target = FilePath.Undefined;

            // act
            var actual = target.Exists;

            // assert
            actual.Should().Be(false);
        }
    }
}