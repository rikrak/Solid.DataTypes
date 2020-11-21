using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.DateRangeTests
{
    [TestClass]
    public class ElapsedDaysTests : TestBase
    {
        [DataTestMethod]
        [DataRow("2017-10-01", "2017-10-01", RangeInclusion.Inclusive, 1, DisplayName = "Inclusive - Zero day range")]
        [DataRow("2017-10-30", "2017-11-01", RangeInclusion.Inclusive, 3, DisplayName = "Inclusive - range over end of long month")]
        [DataRow("2017-09-30", "2017-10-01", RangeInclusion.Inclusive, 2, DisplayName = "Inclusive - range over end of short month")]
        [DataRow("2017-02-27", "2017-03-02", RangeInclusion.Inclusive, 4, DisplayName = "Inclusive - range over end of feb in non leap-year")]
        [DataRow("2016-02-27", "2016-03-02", RangeInclusion.Inclusive, 5, DisplayName = "Inclusive - range over end of feb in leap-year")]

        [DataRow("2017-10-01", "2017-10-01", RangeInclusion.IncludeStart, 0, DisplayName = "IncludeStart - Zero day range")]
        [DataRow("2017-10-30", "2017-11-01", RangeInclusion.IncludeStart, 2, DisplayName = "IncludeStart - range over end of long month")]
        [DataRow("2017-09-30", "2017-10-01", RangeInclusion.IncludeStart, 1, DisplayName = "IncludeStart - range over end of short month")]
        [DataRow("2017-02-27", "2017-03-02", RangeInclusion.IncludeStart, 3, DisplayName = "IncludeStart - range over end of feb in non leap-year")]
        [DataRow("2016-02-27", "2016-03-02", RangeInclusion.IncludeStart, 4, DisplayName = "IncludeStart - range over end of feb in leap-year")]

        [DataRow("2017-10-01", "2017-10-01", RangeInclusion.IncludeEnd, 0, DisplayName = "IncludeEnd - Zero day range")]
        [DataRow("2017-10-30", "2017-11-01", RangeInclusion.IncludeEnd, 2, DisplayName = "IncludeEnd - range over end of long month")]
        [DataRow("2017-09-30", "2017-10-01", RangeInclusion.IncludeEnd, 1, DisplayName = "IncludeEnd - range over end of short month")]
        [DataRow("2017-02-27", "2017-03-02", RangeInclusion.IncludeEnd, 3, DisplayName = "IncludeEnd - range over end of feb in non leap-year")]
        [DataRow("2016-02-27", "2016-03-02", RangeInclusion.IncludeEnd, 4, DisplayName = "IncludeEnd - range over end of feb in leap-year")]

        [DataRow("2017-10-01", "2017-10-01", RangeInclusion.Exclusive, 0, DisplayName = "Exclusive - Zero day range")]
        [DataRow("2017-10-30", "2017-11-01", RangeInclusion.Exclusive, 1, DisplayName = "Exclusive - range over end of long month")]
        [DataRow("2017-09-30", "2017-10-01", RangeInclusion.Exclusive, 0, DisplayName = "Exclusive - range over end of short month")]
        [DataRow("2017-02-27", "2017-03-02", RangeInclusion.Exclusive, 2, DisplayName = "Exclusive - range over end of feb in non leap-year")]
        [DataRow("2016-02-27", "2016-03-02", RangeInclusion.Exclusive, 3, DisplayName = "Exclusive - range over end of feb in leap-year")]
        public void RunElapsedDaysTest(string fromDate, string toDate, RangeInclusion inclusion, int expected)
        {
            // arrange
            var from = GetDate(fromDate);
            var to = GetDate(toDate);

            var target = new DateRange(from, to, inclusion);

            // act
            var actual = target.ElapsedDays();

            // assert
            var assertion = $"The range is {inclusion} so the days between {fromDate} and {toDate} should be {expected}";
            actual.Should().Be(expected, because: assertion);
        }

        private static DateTime GetDate(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", null);
        }

    }
}