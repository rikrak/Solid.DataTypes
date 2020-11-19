using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class MultiplicationTests
    {
        [TestMethod]
        public void Multiplication()
        {
            var half = new Ratio(1,2);
            var twice = new Ratio(2);

            RunMultiplicationTest(1, half, 0.5m);
            RunMultiplicationTest(2, half, 1m);
            RunMultiplicationTest(3, half, 1.5m);
            RunMultiplicationTest(1, twice, 2m);
            RunMultiplicationTest(2, twice, 4m);
            RunMultiplicationTest(3, twice, 6m);
        }

        private void RunMultiplicationTest(decimal value, Ratio ratio, decimal expected)
        {
            // arrange

            // act
            var actual1 = value * ratio;
            var actual2 = ratio * value;

            // assert
            actual1.Should().Be(expected, "{0} x {1} = {2}", value, ratio, expected);
            actual2.Should().Be(expected, "{0} x {1} = {2}", ratio, value, expected);

        }
    }
}
