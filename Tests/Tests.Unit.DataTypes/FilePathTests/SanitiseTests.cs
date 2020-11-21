using System;
using System.Linq;
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

        [TestMethod]
        public void FileNameHasInvalidCharAtEnd()
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars().Where(c =>
                c != System.IO.Path.DirectorySeparatorChar && c != System.IO.Path.AltDirectorySeparatorChar);

            foreach (var invalidChar in invalidChars)
            {
                var original = $"C:\\Temp\\MyFile{invalidChar}.txt";
                var expected = $"C:\\Temp\\MyFile_.txt";

                RunTest(original, expected);

            }
        }

        [TestMethod]
        public void FileNameHasInvalidCharAtStart()
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars().Where(c =>
                c != System.IO.Path.DirectorySeparatorChar && c != System.IO.Path.AltDirectorySeparatorChar);

            foreach (var invalidChar in invalidChars)
            {
                var original = $"C:\\Temp\\{invalidChar}MyFile.txt";
                var expected = $"C:\\Temp\\_MyFile.txt";

                RunTest(original, expected);

            }
        }

        [TestMethod]
        public void FileNameHasInvalidCharInMiddle()
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars().Where(c =>
                c != System.IO.Path.DirectorySeparatorChar && c != System.IO.Path.AltDirectorySeparatorChar);

            foreach (var invalidChar in invalidChars)
            {
                var original = $"C:\\Temp\\My{invalidChar}File.txt";
                var expected = $"C:\\Temp\\My_File.txt";

                RunTest(original, expected);

            }
        }

        [TestMethod]
        public void FileNameHasMultipleInvalidChars()
        {
            var invalidChars = System.IO.Path.GetInvalidFileNameChars().Where(c =>
                c != System.IO.Path.DirectorySeparatorChar && c != System.IO.Path.AltDirectorySeparatorChar);

            foreach (var invalidChar in invalidChars)
            {
                var original = $"C:\\Temp\\My{invalidChar}File{invalidChar}.txt";
                var expected = $"C:\\Temp\\My_File_.txt";

                RunTest(original, expected);

            }
        }

        private void RunTest(string original, string expected)
        {
            // arrange

            // act
            var actual = FilePath.Sanitise(original);

            // assert
            actual.Should().Be(new FilePath(expected));
        }

    }

}
