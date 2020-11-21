using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.ExtentTests
{
    [TestClass]
    public class BuilderTests : TestBase
    {
        [TestMethod]
        public void SetupInclusiveRange()
        {
            // arrange
            var from = 1;
            var to = 5;

            // act
            var actual =new Extent<int>(0, 100, RangeInclusion.Inclusive)
                .WithEffectiveFrom(from)
                .WithEffectiveTo(to)
                .Inclusive();

            // assert
            actual.Should().Be(new Extent<int>(from, to, RangeInclusion.Inclusive));
        }

        [TestMethod]
        public void SetupExclusiveRange()
        {
            // arrange
            var from = 1;
            var to = 15;

            // act
            var actual =new Extent<int>(0, 100, RangeInclusion.Inclusive)
                .WithEffectiveFrom(from)
                .WithEffectiveTo(to)
                .Exclusive();

            // assert
            actual.Should().Be(new Extent<int>(from, to, RangeInclusion.Exclusive));
        }

    }
}