using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.MimeTypeMapTests
{
    [TestClass]
    public class GetExtensionsForTests : TestBase
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
            var actual = this._target.GetExtensionsFor(type);

            // assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void TypeHasManyExtensions()
        {
            // arrange
            var type = MimeType.Default;

            // act
            var actual = this._target.GetExtensionsFor(type);

            // assert
            actual.Should().Contain(FileExtension.For(".bin"));
            actual.Should().Contain(FileExtension.For(".dms"));
            actual.Should().Contain(FileExtension.For(".lrf"));
            actual.Should().Contain(FileExtension.For(".mar"));
            actual.Should().Contain(FileExtension.For(".so"));
            actual.Should().Contain(FileExtension.For(".dist"));
            actual.Should().Contain(FileExtension.For(".distz"));
            actual.Should().Contain(FileExtension.For(".pkg"));
            actual.Should().Contain(FileExtension.For(".bpk"));
            actual.Should().Contain(FileExtension.For(".dump"));
            actual.Should().Contain(FileExtension.For(".elc"));
            actual.Should().Contain(FileExtension.For(".deploy"));
        }

        [TestMethod]
        public void TypeHasSingleExtension()
        {
            // arrange
            var type = new MimeType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            // act
            var actual = this._target.GetExtensionsFor(type);

            // assert
            actual.Should().HaveCount(1);
            actual.Should().Contain(FileExtension.For(".xlsx"));
        }

    }
}