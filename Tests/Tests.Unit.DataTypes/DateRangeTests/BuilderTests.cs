using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.DateRangeTests
{
    [TestClass]
    public class BuilderTests : TestBase
    {
        [TestMethod]
        public void SetupInclusiveRange()
        {
            // arrange
            var from = 01.January(1980);
            var to = 15.May(1993);

            // act
            var actual =new DateRange()
                .WithEffectiveFrom(from)
                .WithEffectiveTo(to)
                .Inclusive();

            // assert
            actual.Should().Be(new DateRange(from, to, RangeInclusion.Inclusive));
        }

        [TestMethod]
        public void SetupExclusiveRange()
        {
            // arrange
            var from = 01.January(1980);
            var to = 15.May(1993);

            // act
            var actual =new DateRange()
                .WithEffectiveFrom(from)
                .WithEffectiveTo(to)
                .Exclusive();

            // assert
            actual.Should().Be(new DateRange(from, to, RangeInclusion.Exclusive));
        }

        [TestMethod]
        public void ShouldIgnoreTimeElement()
        {
            // arrange
            var from = 01.January(1980).At(14.Hours(23.Minutes()));
            var to = 15.May(1993).At(9.Hours(56.Minutes()));

            // act
            var actual =new DateRange()
                .WithEffectiveFrom(from)
                .WithEffectiveTo(to)
                .Exclusive();

            // assert
            actual.Should().Be(new DateRange(from.Date, to.Date, RangeInclusion.Exclusive));
        }
    }
}