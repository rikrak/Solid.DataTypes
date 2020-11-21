using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.DateRangeTests
{
    [TestClass]
    public class IsInRangeTests : TestBase
    {
        [DataTestMethod]
        [DataRow("2017-10-01", "2017-10-01", "2017-10-01", RangeInclusion.Inclusive, true, DisplayName = "Inclusive - Zero day range - on same day")]
        [DataRow("2017-10-01", "2017-10-01", "2017-09-30", RangeInclusion.Inclusive, false, DisplayName = "Inclusive - Zero day range - before range")]
        [DataRow("2017-10-01", "2017-10-01", "2017-10-02", RangeInclusion.Inclusive, false, DisplayName = "Inclusive - Zero day range - after range")]

        [DataRow("2017-10-01", "2017-11-01", "2017-09-30", RangeInclusion.Inclusive, false, DisplayName = "Inclusive - before start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-01", RangeInclusion.Inclusive, true, DisplayName = "Inclusive - on start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-02", RangeInclusion.Inclusive, true, DisplayName = "Inclusive - in range")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-01", RangeInclusion.Inclusive, true, DisplayName = "Inclusive - on end day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-02", RangeInclusion.Inclusive, false, DisplayName = "Inclusive - after end day")]

        [DataRow("2017-10-01", "2017-11-01", "2017-09-30", RangeInclusion.IncludeStart, false, DisplayName = "IncludeStart - before start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-01", RangeInclusion.IncludeStart, true, DisplayName = "IncludeStart - on start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-02", RangeInclusion.IncludeStart, true, DisplayName = "IncludeStart - in range")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-01", RangeInclusion.IncludeStart, false, DisplayName = "IncludeStart - on end day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-02", RangeInclusion.IncludeStart, false, DisplayName = "IncludeStart - after end day")]

        [DataRow("2017-10-01", "2017-11-01", "2017-09-30", RangeInclusion.IncludeEnd, false, DisplayName = "IncludeEnd - before start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-01", RangeInclusion.IncludeEnd, false, DisplayName = "IncludeEnd - on start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-02", RangeInclusion.IncludeEnd, true, DisplayName = "IncludeEnd - in range")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-01", RangeInclusion.IncludeEnd, true, DisplayName = "IncludeEnd - on end day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-02", RangeInclusion.IncludeEnd, false, DisplayName = "IncludeEnd - after end day")]

        [DataRow("2017-10-01", "2017-11-01", "2017-09-30", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - before start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-01", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - on start day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-10-02", RangeInclusion.Exclusive, true, DisplayName = "Exclusive - in range")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-01", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - on end day")]
        [DataRow("2017-10-01", "2017-11-01", "2017-11-02", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - after end day")]

        [DataRow("2017-10-01", "2017-10-01", "2017-10-01", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - Zero day range - on same day")]
        [DataRow("2017-10-01", "2017-10-01", "2017-09-30", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - Zero day range - before range")]
        [DataRow("2017-10-01", "2017-10-01", "2017-10-02", RangeInclusion.Exclusive, false, DisplayName = "Exclusive - Zero day range - after range")]
        public void RunIsInRangeTest(string fromDate, string toDate, string candidateDate, RangeInclusion inclusion, bool expected)
        {
            // arrange
            var from = GetDate(fromDate);
            var to = GetDate(toDate);
            var candidate = GetDate(candidateDate);

            var target = new DateRange(from, to, inclusion);

            // act
            var actual = target.IsInRange(candidate);

            // assert
            var assertion = $"The range is {inclusion} so {candidateDate} should be {(expected ? "in" : "out")}side the range ({target})";
            actual.Should().Be(expected, because: assertion);
        }

        private static DateTime GetDate(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", null);
        }

    }
}
