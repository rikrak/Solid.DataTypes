using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.FileExtensionTests
{
    [TestClass]
    public class CtorTests
    {
        [DataRow("", DisplayName = "Empty")]
        [DataRow(null, DisplayName = "Null Value")]
        [DataRow("    \t", DisplayName = "Whitespace")]
        [TestMethod]
        public void ValueIsRequired(string value)
        {
            // arrange

            // act
            Action act = () => new FileExtension(value);

            // assert
            act.Should().Throw<ArgumentException>();
        }

        [DataRow("txt")]
        [DataRow("   txt")]
        [DataRow("   txt    ")]
        [DataRow("txt    ")]
        [DataRow(".txt")]
        [DataRow("   .txt")]
        [DataRow("   .txt   ")]
        [DataRow(".txt   ")]
        [TestMethod]
        public void ShouldStandardiseValue(string value)
        {
            // arrange

            // act
            var actual = new FileExtension(value);

            // assert
            actual.ToString().Should().Be(".txt");
            actual.Should().Be(FileExtension.For(".txt"));
        }

    }
}
