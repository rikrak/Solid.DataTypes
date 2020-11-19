using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.FactorTests
{
    [TestClass]
    public class ReduceTests
    {
        [TestMethod]
        public void FactorIsAnInteger()
        {
            // arrange
            var target = new Factor(2);

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("2");
        }

        [TestMethod]
        public void FactorIsAlreadyReduced()
        {
            // arrange
            var target = new Factor(1, 2);

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("1/2");
        }

        [TestMethod]
        public void FactorIsAlreadyReduced_LowNumber()
        {
            // arrange
            var target = new Factor(3, 11);

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("3/11");
        }

        [TestMethod]
        public void FactorIsAlreadyReduced_HighValue()
        {
            // arrange
            var target = new Factor(397, 9973);  // use prime numbers

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("397/9973");
        }

        [TestMethod]
        public void FactorIsHalf()
        {
            // arrange
            var target = new Factor(5, 10);

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("1/2");
        }

        [TestMethod]
        public void FactorIsThird()
        {
            // arrange
            var target = new Factor(33, 99);

            // act
            var actual = target.Reduce();

            // assert
            actual.Should().Be(target, because: "the value of the factor should not change");
            actual.ToString().Should().Be("1/3");
        }
    }
}