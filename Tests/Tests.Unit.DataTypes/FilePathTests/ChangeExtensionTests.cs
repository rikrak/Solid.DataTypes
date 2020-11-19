using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.FilePathTests
{
    [TestClass]
    public class ChangeExtensionTests : TestBase
    {
        [TestMethod]
        public void FilenameWithDoubleBarrelExtension()
        {
            // arrange
            var target = new FilePath("YearEndReport.2020.docx");

            // act
            var actual = target.ChangeExtension(FileExtension.For(".xlsx"));

            // assert
            actual.Should().Be(new FilePath("YearEndReport.2020.xlsx"));
        }

        [TestMethod]
        public void JustHasRegularFileName()
        {
            // arrange
            var target = new FilePath("YearEndReport.docx");

            // act
            var actual = target.ChangeExtension(FileExtension.For(".xlsx"));

            // assert
            actual.Should().Be(new FilePath("YearEndReport.xlsx"));
        }

        [TestMethod]
        public void HasFileNameWithoutExtension()
        {
            // arrange
            var target = new FilePath("YearEndReport");

            // act
            var actual = target.ChangeExtension(FileExtension.For(".docx"));

            // assert
            actual.Should().Be(new FilePath("YearEndReport.docx"));
        }

        [TestMethod]
        public void HasRegularFileNameAndPath()
        {
            // arrange
            var target = new FilePath("C:\\My Documents\\YearEndReport.docx");

            // act
            var actual = target.ChangeExtension(FileExtension.For(".xlsx"));

            // assert
            actual.Should().Be(new FilePath("C:\\My Documents\\YearEndReport.xlsx"));
        }

        [TestMethod]
        public void HasFileNameWithoutExtensionAndPath()
        {
            // arrange
            var target = new FilePath("C:\\My Documents\\YearEndReport");

            // act
            var actual = target.ChangeExtension(FileExtension.For(".docx"));

            // assert
            actual.Should().Be(new FilePath("C:\\My Documents\\YearEndReport.docx"));
        }

    }
}