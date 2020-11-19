using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.FactorTests
{
    [TestClass]
    public class ToStringTests
    {
        [TestMethod]
        public void ShouldOutputFraction()
        {
            RunTest(1, 2, "1/2");
            RunTest(1, 3, "1/3");
            RunTest(2, 1, "2");
            RunTest(9, 3, "9/3");
            RunTest(2, 2, "2/2");
            RunTest(75, 100, "75/100");
            RunTest(7500, 10000, "7500/10000");
        }

        private static void RunTest(int numerator, int denominator, string expected)
        {
            // arrange
            var target = new Factor(numerator, denominator);

            // act
            var actual = target.ToString();

            // assert
            actual.Should().Be(expected);
        }
    }
}