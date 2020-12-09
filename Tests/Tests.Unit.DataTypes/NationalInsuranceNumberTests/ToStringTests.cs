using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.NationalInsuranceNumberTests
{
    [TestClass]
    public class ToStringTests
    {
        [DataRow("AB123456D", "AB123456D")]
        [DataRow("AB 12 34 56 D", "AB123456D")]
        [DataRow("ab123456d", "AB123456D")]
        [DataRow("ab 12 34 56 d", "AB123456D")]
        [DataRow("ab12 34 56d", "AB123456D")]
        [DataRow("ab 123456 d", "AB123456D")]
        [DataRow("a b 1 2 3 4 5 6 d", "AB123456D")]
        [DataTestMethod]
        public void ShouldBeUpperCaseNoSpace(string value, string expected)
        {
            // arrange
            var item = new NationalInsuranceNumber(value);

            // act
            var actual = item.ToString();
            
            // assert
            actual.Should().Be(expected);
        }

        [DataRow("AB123456D", "AB123456D")]
        [DataRow("AB 12 34 56 D", "AB123456D")]
        [DataRow("ab123456d", "AB123456D")]
        [DataRow("ab 12 34 56 d", "AB123456D")]
        [DataRow("ab12 34 56d", "AB123456D")]
        [DataRow("ab 123456 d", "AB123456D")]
        [DataRow("a b 1 2 3 4 5 6 d", "AB123456D")]
        [DataTestMethod]
        public void ShouldBeUpperCaseNoSpaceWhenNoFormatSpecifier(string value, string expected)
        {
            // arrange
            var item = new NationalInsuranceNumber(value);

            // act
            var actual = item.ToString(null, null);
            
            // assert
            actual.Should().Be(expected);
        }

        [DataRow("AB123456D", "AB123456D")]
        [DataRow("ab123456d", "AB123456D")]
        [DataRow("AB 12 34 56 D", "AB123456D")]
        [DataRow("ab 12 34 56 d", "AB123456D")]
        [DataRow("ab12 34 56d", "AB123456D")]
        [DataRow("ab 123456 d", "AB123456D")]
        [DataRow("a b 1 2 3 4 5 6 d", "AB123456D")]
        [DataTestMethod]
        public void CompactFormat_ShouldBeUpperCaseNoSpace(string value, string expected)
        {
            // arrange
            var item = new NationalInsuranceNumber(value);

            // act
            var actualExplicit = item.ToString("C", null);
            var actualInterpolated = $"{item:C}";
            var actualFormatted = string.Format("{0:C}", item);

            // assert
            actualExplicit.Should().Be(expected);
            actualFormatted.Should().Be(expected);
            actualInterpolated.Should().Be(expected);
        }

        [DataRow("AB 12 34 56 D", "AB 12 34 56 D")]
        [DataRow("AB123456D", "AB 12 34 56 D")]
        [DataRow("ab123456d", "AB 12 34 56 D")]
        [DataRow("ab 12 34 56 d", "AB 12 34 56 D")]
        [DataRow("ab12 34 56d", "AB 12 34 56 D")]
        [DataRow("ab 123456 d", "AB 12 34 56 D")]
        [DataRow("a b 1 2 3 4 5 6 d", "AB 12 34 56 D")]
        [DataTestMethod]
        public void ExpandedFormat_ShouldBeUpperCaseNoSpace(string value, string expected)
        {
            // arrange
            var item = new NationalInsuranceNumber(value);

            // act
            var actualExplicit = item.ToString("E", null);
            var actualInterpolated = $"{item:E}";
            var actualFormatted = string.Format("{0:E}", item);

            // assert
            actualExplicit.Should().Be(expected);
            actualFormatted.Should().Be(expected);
            actualInterpolated.Should().Be(expected);
        }

        [TestMethod]
        public void InvalidFormatSpecified()
        {
            // arrange
            var item = new NationalInsuranceNumber("AB 12 34 56 D");
            string x;

            // act
            Action actExplicit = () => x =item.ToString("x", null);
            Action actInterpolated = () => x = $"{item:x}";
            Action actFormatted = () => x = string.Format("{0:x}", item);

            // assert
            actExplicit.Should().Throw<FormatException>();
            actFormatted.Should().Throw<FormatException>();
            actInterpolated.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void EmptyValue()
        {
            // arrange
            
            // act
            var actual = NationalInsuranceNumber.Empty.ToString();

            // assert
            actual.Should().BeEmpty();
        }

    }
}