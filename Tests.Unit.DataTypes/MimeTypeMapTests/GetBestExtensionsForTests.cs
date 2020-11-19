using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.MimeTypeMapTests
{
    [TestClass]
    public class GetBestExtensionsForTests : TestBase
    {
        private MimeTypeMap _target;

        public override void Setup()
        {
            base.Setup();
            _target = new MimeTypeMap();
        }

        [TestMethod]
        public void TypeNotInMap()
        {
            // arrange
            var type = new MimeType("crisps/plain");

            // act
            var actual = this._target.GetBestExtensionsFor(type);

            // assert
            actual.Should().Be(FileExtension.None);
        }

        [TestMethod]
        public void TypeHasManyExtensions()
        {
            // arrange
            var type = MimeType.Default;

            // act
            var actual = this._target.GetBestExtensionsFor(type);

            // assert
            actual.Should().Be(FileExtension.For(".bin"));
        }

        [TestMethod]
        public void TypeHasSingleExtension()
        {
            // arrange
            var type = new MimeType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            // act
            var actual = this._target.GetBestExtensionsFor(type);

            // assert
            actual.Should().Be(FileExtension.For(".xlsx"));
        }

    }


}
