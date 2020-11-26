using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.DateRangeTests
{
    [TestClass]
    public class CtorTests
    {
        [TestMethod]
        public void FromShouldBeBeforeTo()
        {
            // arrange
            var from = 01.January(2020);
            var to = from.AddYears(-1);

            // act
            Action act = () => new DateRange(from, to, RangeInclusion.Inclusive);

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
            var from = DateTime.Today;
            var to = from.AddYears(1);

            // act
            var target = new DateRange(from, to, inclusion);

            // assert
            target.From.Should().Be(from);
            target.To.Should().Be(to);
            target.IsInclusive.Should().Be((inclusion & RangeInclusion.Inclusive) == RangeInclusion.Inclusive);
            target.IncludesEnd.Should().Be((inclusion & RangeInclusion.IncludeEnd) == RangeInclusion.IncludeEnd);
            target.IncludesStart.Should().Be((inclusion & RangeInclusion.IncludeStart) == RangeInclusion.IncludeStart);
        }


    }
}
