using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.MimeTypeMapTests
{
    [TestClass]
    public class GetMimeTypeForTests : TestBase
    {
        private MimeTypeMap _target;

        public override void Setup()
        {
            base.Setup();
            _target = new MimeTypeMap();
        }

        [TestMethod]
        public void ExtensionNotInMap()
        {
            // arrange
            var ext = FileExtension.For(".solid");

            // act
            var actual = this._target.GetMimeTypeFor(ext);

            // assert
            actual.Should().Be(MimeType.Default);
        }

        [TestMethod]
        public void ExtensionInMapWithCommonType()
        {
            // arrange
            var ext = FileExtension.For(".dump");

            // act
            var actual = this._target.GetMimeTypeFor(ext);

            // assert
            actual.Should().Be(new MimeType("application/octet-stream"));
        }

        [TestMethod]
        public void ExtensionHasSingleTypeMap()
        {
            // arrange
            var ext = FileExtension.For(".xlsx");

            // act
            var actual = this._target.GetMimeTypeFor(ext);

            // assert
            actual.Should().Be(new MimeType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));
        }
    }
}