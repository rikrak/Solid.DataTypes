using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class ToStringTests
    {
        [TestMethod]
        public void DefaultValue()
        {
            // arrange
            var target = default(Ratio);

            // act
            var actual = target.ToString();

            // assert
            actual.Should().Be("0.00");
        }

        [TestMethod]
        public void DenominatorIsOne()
        {
            // arrange
            var target = new Ratio(0.5m);

            // act
            var actual = target.ToString();

            // assert
            actual.Should().Be("0.50");
        }


        [DataRow(1, 3, "0.33")]
        [DataRow(3, 1, "3.00")]
        [DataRow(3, 3, "1.00")]
        [TestMethod]
        public void RunTest(double numerator, double denominator, string expected)
        {
            // arrange
            var target = new Ratio((decimal)numerator, (decimal)denominator);

            // act
            var actual = target.ToString();

            // assert
            actual.Should().Be(expected);
        }

    }
}