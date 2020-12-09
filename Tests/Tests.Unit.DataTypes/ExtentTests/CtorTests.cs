using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.ExtentTests
{
    [TestClass]
    public class CtorTests
    {
        [TestMethod]
        public void FromValueShouldNotBeNull()
        {
            // arrange
            string from = null;
            string to = "not null";

            // act
            Action act = () => new Extent<string>(from, to, RangeInclusion.Inclusive);

            // assert
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("from");
        }

        [TestMethod]
        public void ToValueShouldNotBeNull()
        {
            // arrange
            string from = "not null";
            string to = null;

            // act
            Action act = () => new Extent<string>(from, to, RangeInclusion.Inclusive);

            // assert
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("to");
        }

        [TestMethod]
        public void FromShouldBeBeforeTo()
        {
            // arrange
            string from = "Bravo";
            string to = "Alpha";

            // act
            Action act = () => new Extent<string>(from, to, RangeInclusion.Inclusive);

            // assert
            act.Should().Throw<ArgumentException>().And.ParamName.Should().Be("to");
        }

        [DataRow(RangeInclusion.IncludeEnd)]
        [DataRow(RangeInclusion.IncludeStart)]
        [DataRow(RangeInclusion.Exclusive)]
        [DataRow(RangeInclusion.Inclusive)]
        [TestMethod]
        public void ShouldSetProperties(RangeInclusion inclusion)
        {
            // arrange
            const int from = 10;
            const int to = 100;

            // act
            var actual = new Extent<int>(from, to, inclusion);

            // assert
            actual.From.Should().Be(from);
            actual.To.Should().Be(to);

            actual.IncludesStart.Should().Be(inclusion.HasFlag(RangeInclusion.IncludeStart));
            actual.IncludesEnd.Should().Be(inclusion.HasFlag(RangeInclusion.IncludeEnd));
            actual.IsInclusive.Should().Be(inclusion.HasFlag(RangeInclusion.IncludeStart) && inclusion.HasFlag(RangeInclusion.IncludeEnd));
        }

    }
}
