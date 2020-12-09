using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class CtorTests
    {
        [TestMethod]
        public void ShouldPreventDivideByZero()
        {
            // arrange
            
            // act
            Action act = () => new Ratio(12, 0);

            // assert
            act.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void DefaultConstructorShouldEquateToZero()
        {
            // arrange
            
            // act
            var actual1 = new Ratio();
            var actual2 = default(Ratio);

            // assert
            ((decimal)actual1).Should().Be(0m);
            ((decimal)actual2).Should().Be(0m);
        }

    }
    [TestClass]
    public class ApplyToTests
    {
        [TestMethod]
        public void UseDefaultValue()
        {
            // arrange
            var target = new Ratio();

            // act
            var actual = target.ApplyTo(3);

            // assert
            actual.Should().Be(0m);
        }


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
