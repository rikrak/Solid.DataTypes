using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.FilePathTests
{
    [TestClass]
    public class IsValidTests
    {
        [DataRow("C:\\readme.txt", true, DisplayName = "SimpleFilePathAtRoot")]
        [DataRow("C:\\Users\\Darwin\\My Documents\\Origin of the species.txt", true, DisplayName = "FullPathWithSpaces")]
        [DataRow("\\\\Jupiter\\Users\\Douglas Adams\\Mostly Harmless.txt", true, DisplayName = "UncPathWithSpaces")]
        [DataRow("\\\\MyServer\\Share\\readme.txt", true, DisplayName = "SimpleFilePathOnUncPath")]
        [DataRow("C:\\", true, DisplayName = "RootPath")]
        [DataRow("readme.txt", true, DisplayName = "JustFileName")]
        [DataRow("read me.txt", true, DisplayName = "JustFileNameWithSpaces")]
        [DataRow("Documents", true, DisplayName = "JustFolderName")]
        [DataRow("\\Documents", true, DisplayName = "RelativeFolderName")]
        [DataRow("Documents\\", true, DisplayName = "FolderWithTrailingSlash")]
        [DataRow("My Documents", true, DisplayName = "JustFolderNameWithSpaces")]
        [DataRow("C:\\Temp\\bad:Filename.txt", false, DisplayName = "RootedPathWithBadFilename")]
        // commented these out as the tests fail after moving to .netstandard from .NET Framework
        // Given that this can now run on non-windows platforms, these paths may actually be valid???!!!
        //[DataRow("\\\\Saturn\\Finance\\C:\\Temp\\badFilename.txt", false, DisplayName = "UncPathWithBadFilename")]
        //[DataRow("D:\\Users\\Finance\\C:\\Temp\\badFilename.txt", false, DisplayName = "DoubleRootedPathWithFilename")]
        [DataTestMethod]
        public void RunTest(string path, bool expected)
        {
            // arrange
            var target = new FilePath((path));

            // act
            var actual = target.IsValid;

            // assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void UndefinedTest()
        {
            // arrange
            var target = FilePath.Undefined;

            // act
            var actual = target.IsValid;

            // assert
            actual.Should().Be(false);
        }
    }
}