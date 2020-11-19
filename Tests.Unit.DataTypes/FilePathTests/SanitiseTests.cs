using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.FilePathTests
{
    [TestClass]
    public class SanitiseTests : TestBase
    {
        [TestMethod]
        public void FileNameIsValid()
        {
            // arrange

            // act
            var actual = FilePath.Sanitise("C:\\Temp\\MyFile.txt");

            // assert
            actual.Should().Be(new FilePath("C:\\Temp\\MyFile.txt"));
        }

        [DataRow("C:\\Temp\\MyFile?.txt", "C:\\Temp\\MyFile_.txt")]
        [DataRow("MyFile?.txt", "MyFile_.txt")]
        [DataRow("MyFile.txt", "MyFile.txt")]
        [DataRow("C:\\Temp\\My>File?.txt", "C:\\Temp\\My_File_.txt")]
        [DataRow("C:\\Te\tmp\\MyFile.txt", "C:\\Te_mp\\MyFile.txt")]
        [DataRow("C:\\Temp\\My:File.txt", "C:\\Temp\\My_File.txt")]
        [DataRow("C:\\Temp\\Re:File.txt.pdf", "C:\\Temp\\Re_File.txt.pdf")]
        [DataRow("C:\\Temp\\Re:File", "C:\\Temp\\Re_File")]
        [DataRow("C:\\Temp\\Re:>File", "C:\\Temp\\Re__File")]
        [DataRow("C:\\Temp\\Re>:File", "C:\\Temp\\Re__File")]
        [DataTestMethod]
        public void FileNameHasInvalidChar(string original, string expected)
        {
            // arrange

            // act
            var actual = FilePath.Sanitise(original);

            // assert
            actual.Should().Be(new FilePath(expected));
        }

    }

}
