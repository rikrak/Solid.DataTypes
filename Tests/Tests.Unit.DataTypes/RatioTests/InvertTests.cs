using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class InvertTests
    {
        [DataRow(4, 5)]
        [DataRow(1, 3)]
        [DataRow(3, 1)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataTestMethod]
        public void RunInvertTest(int numerator, int denominator)
        {
            // arrange
            var target = new Ratio(numerator, denominator);
            var expected = new Ratio(denominator, numerator);

            // act
            var actual = target.Invert();

            // assert
            ((decimal)actual).Should().Be((decimal)expected);
        }

        [TestMethod]
        public void InvertDefault()
        {
            // arrange
            var defaultVal = default(Ratio);

            // act
            var actual = defaultVal.Invert();

            // assert
            ((decimal) actual).Should().Be(0m);
        }

    }
}