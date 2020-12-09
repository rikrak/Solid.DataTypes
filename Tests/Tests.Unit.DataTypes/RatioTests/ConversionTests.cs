using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class ConversionTests
    {
        [DataTestMethod]
        [DataRow(100, 100,   1, DisplayName = "numerator equals denominator")]
        [DataRow(200, 100,   2, DisplayName = "numerator Is greater than denominator")]
        [DataRow(50,  100, 0.5, DisplayName = "numerator is less than denominator")]
        [DataRow(0,   100,   0, DisplayName = "numerator is zero")]
        public void CastOperatorTests(int numerator, int denominator, double expected)
        {
            // arrange
            var expectedAsDecimal = (decimal) expected; // decimals cannot be used in attributes, hence cast from double
            var target = new Ratio(numerator, denominator);
            
            // act
            var actual = (decimal) target;

            // assert
            actual.Should().Be(expectedAsDecimal);
        }

        [TestMethod]
        public void CastToNullableDecimalWithNullValue()
        {
            // arrange
            Ratio? nullValue = null;

            // act
            var actual = (decimal?) nullValue;

            // assert
            actual.Should().BeNull();

        }

        [DataTestMethod]
        [DataRow(100, 100, 1, DisplayName = "numerator equals denominator")]
        [DataRow(200, 100, 2, DisplayName = "numerator Is greater than denominator")]
        [DataRow(50, 100, 0.5, DisplayName = "numerator is less than denominator")]
        [DataRow(0, 100, 0, DisplayName = "numerator is zero")]
        public void CastToNullableFromNullableTests(int numerator, int denominator, double expected)
        {
            // arrange
            var expectedAsDecimal = (decimal)expected; // decimals cannot be used in attributes, hence cast from double
            Ratio? target = new Ratio(numerator, denominator);

            // act
            var actual = (decimal?)target;

            // assert
            actual.Should().Be(expectedAsDecimal);
        }

        [DataTestMethod]
        [DataRow(100, 100, 1, DisplayName = "numerator equals denominator")]
        [DataRow(200, 100, 2, DisplayName = "numerator Is greater than denominator")]
        [DataRow(50, 100, 0.5, DisplayName = "numerator is less than denominator")]
        [DataRow(0, 100, 0, DisplayName = "numerator is zero")]
        public void CastToNullableTests(int numerator, int denominator, double expected)
        {
            // arrange
            var expectedAsDecimal = (decimal)expected; // decimals cannot be used in attributes, hence cast from double
            var target = new Ratio(numerator, denominator);

            // act
            var actual = (decimal?)target;

            // assert
            actual.Should().Be(expectedAsDecimal);
        }

        [TestMethod]
        public void CastDefaultValue()
        {
            // arrange
            var target = default(Ratio);

            // act
            var actual = (decimal) target;

            // assert
            actual.Should().Be(0m);
        }

        [TestMethod]
        public void CastDefaultValueAsNullable()
        {
            // arrange
            Ratio? target = default(Ratio);

            // act
            var actual = (decimal) target;

            // assert
            actual.Should().Be(0m);
        }

    }
}