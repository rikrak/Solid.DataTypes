using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class ApplyToTests
    {
        [TestMethod]
        public void ShouldBeReversible()
        {
            RunReversibleRatioTest(39000, 57705);
            RunReversibleRatioTest(10, 30);
            RunReversibleRatioTest(1, 3);
            RunReversibleRatioTest(2, 3);
            RunReversibleRatioTest(1, 2);
            RunReversibleRatioTest(2, 1);
            RunReversibleRatioTest(3, 1);
        }

        private void RunReversibleRatioTest(decimal numerator, decimal denominator)
        {
            // arrange
            var ratio = new Ratio(numerator, denominator);

            // act
            var actual = ratio.ApplyTo(denominator);

            // assert
            actual.Should().Be(numerator);
        }
    }
}
