using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.NationalInsuranceNumberTests
{
    [TestClass]
    public class ConversionTests
    {
        [DataRow("AB123456D", "AB123456D")]
        [DataRow("AB 12 34 56 D", "AB123456D")]
        [DataRow("ab123456d", "AB123456D")]
        [DataRow("ab 12 34 56 d", "AB123456D")]
        [DataRow("ab12 34 56d", "AB123456D")]
        [DataRow("ab 123456 d", "AB123456D")]
        [DataRow("a b 1 2 3 4 5 6 d", "AB123456D")]
        [DataTestMethod]
        public void TryParse_Valid(string value, string expected)
        {
            // arrange

            // act
            var actual = NationalInsuranceNumber.TryParse(value, out var result);

            // assert
            actual.Should().BeTrue();
            ((string)result).Should().Be(expected);
        }

        [DataTestMethod]
        public void TryParse_Invalid()
        {
            // arrange
            var value = "Not an NI number";
            NationalInsuranceNumber target;

            // act
            var actual = NationalInsuranceNumber.TryParse(value, out target);

            // assert
            actual.Should().BeFalse();
            target.Should().Be(NationalInsuranceNumber.Empty);
        }

        [TestMethod]
        public void CastFromEmpty()
        {
            // arrange
            
            // act
            var actual = (string)NationalInsuranceNumber.Empty;

            // assert
            actual.Should().BeEmpty();
        }
    }
}