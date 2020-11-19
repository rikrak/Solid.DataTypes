using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.RatioTests
{
    [TestClass]
    public class FromTests
    {
        [DataRow( 1,  3, 3000,  1000, DisplayName = "ValueIsOneThird")]
        [DataRow( 2,  3, 3000,  2000, DisplayName = "ValueIsTwoThirds")]
        [DataRow( 1,  6, 3000,   500, DisplayName = "ValueIsOneSixth")]
        [DataRow( 5,  6, 3000,  2500, DisplayName = "ValueIsFiveSixths")]
        [DataRow( 1,  9, 9000,  1000, DisplayName = "ValueIsOneNinth")]
        [DataRow( 1, 28, 1400,    50, DisplayName = "ValueIsOneTwentyEighth")]
        [DataRow( 4,  3, 3000,  4000, DisplayName = "ValueIsFourThirds")]
        [DataRow(-1,  3, 3000, -1000, DisplayName = "ValueIsNegativeOneThird")]
        [DataRow( 1,  4, 4000,  1000, DisplayName = "ValueIsOneQuarter")]
        [DataRow( 1, 10, 4000,   400, DisplayName = "ValueIsOneTenth")]
        [DataTestMethod]
        public void GeneralFromTests(int numerator, int denominator, int appliedTo, int shouldBe)
        {
            // arrange
            var decimalValue = ((decimal)numerator) / denominator;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            actual.ApplyTo(appliedTo).Should().Be(shouldBe);
        }

        [TestMethod]
        public void NonFractionalValue()
        {
            // arrange
            const decimal decimalValue = 0.0121215888647m;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(decimalValue);
        }

        [TestMethod]
        public void ZeroValue()
        {
            // arrange
            const decimal decimalValue = 0;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(0);
        }

        [TestMethod]
        public void IntegerValue()
        {
            // arrange
            const int integerValue = 10;

            // act
            var actual = Ratio.From(integerValue);

            // assert
            ((decimal)actual).Should().Be(integerValue);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_1()
        {
            // arrange
            const decimal decimalValue = 0.333333m;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(0.333333m);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_2()
        {
            // arrange
            const decimal decimalValue = 0.3333333m;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(1m/3);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_3()
        {
            // arrange
            const decimal decimalValue = 0.666667m;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(0.666667m);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_4()
        {
            // arrange
            const decimal decimalValue = 0.6666667m;

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(2m/3);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_5()
        {
            // arrange
            const decimal decimalValue = 0.666666m; // 6dp

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(0.666666m);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_6()
        {
            // arrange
            const decimal decimalValue = 0.6666666m; // 7dp

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(0.6666666m);
        }

        [TestMethod]
        public void TruncatedRecurringFraction_7()
        {
            // arrange
            const decimal decimalValue = 0.66666666m; // 8dp

            // act
            var actual = Ratio.From(decimalValue);

            // assert
            ((decimal)actual).Should().Be(2m/3);
        }
    }
}