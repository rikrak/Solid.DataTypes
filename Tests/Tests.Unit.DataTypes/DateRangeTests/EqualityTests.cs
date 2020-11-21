using System;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.DateRangeTests
{
    [TestClass]
    public class EqualityTests : TestBase
    {
        [TestMethod]
        public void ObjectsAreSame()
        {
            // arrange
            var range = 12.September(2017).To(12.October(2017), RangeInclusion.Inclusive);

            // act & assert
            RunEqualityTest(range, range, true);
        }

        [TestMethod]
        public void CompareToDifferentType()
        {
            // arrange
            var range = 12.September(2017).To(12.October(2017), RangeInclusion.Inclusive);

            // act 
            var actual = range.Equals("not a date range");

            // assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void CompareToNull()
        {
            // arrange
            var range = 12.September(2017).To(12.October(2017), RangeInclusion.Inclusive);


            // act & assert
            RunEqualityTest(range, null, false);
        }

        [DataTestMethod]
        [DataRow("2017-05-01", "2017-07-01", RangeInclusion.Inclusive, "2017-05-01", "2017-07-01", RangeInclusion.Inclusive, true,  DisplayName = "Same Start Dates, Same End Dates, Same Inclusion")]
        [DataRow("2017-01-19", "2017-07-01", RangeInclusion.Inclusive, "2017-05-01", "2017-07-01", RangeInclusion.Inclusive, false, DisplayName = "Different Start Dates, Same End Dates, Same Inclusion")]
        [DataRow("2017-05-01", "2017-07-01", RangeInclusion.Inclusive, "2017-05-01", "2017-11-09", RangeInclusion.Inclusive, false, DisplayName = "Same Start Dates, Different End Dates, Same Inclusion")]
        [DataRow("2017-05-01", "2017-07-01", RangeInclusion.Inclusive, "2017-05-01", "2017-07-01", RangeInclusion.Exclusive, false, DisplayName = "Same Start Dates, Same End Dates, Different Inclusion")]
        [DataRow("2017-01-01", "2017-02-01", RangeInclusion.Inclusive, "2017-05-01", "2017-07-01", RangeInclusion.Inclusive, false, DisplayName = "Different Start Dates, Different End Dates, Same Inclusion")]
        [DataRow("2017-01-01", "2017-02-01", RangeInclusion.Inclusive, "2017-05-01", "2017-07-01", RangeInclusion.Exclusive, false, DisplayName = "Different Start Dates, Different End Dates, Different Inclusion")]
        public void RunEqualityTest(
            string startDateLhs, string endDateLhs, RangeInclusion inclusionLhs, 
            string startDateRhs, string endDateRhs, RangeInclusion inclusionRhs, 
            bool expected
        )
        {
            // arrange
            var lhs = new DateRange(GetDate(startDateLhs), GetDate(endDateLhs), inclusionLhs);
            var rhs = new DateRange(GetDate(startDateRhs), GetDate(endDateRhs), inclusionRhs);

            // act & assert
            RunEqualityTest(lhs, rhs, expected);

        }

        private void RunEqualityTest(DateRange lhs, DateRange rhs, bool areExpectedToBeEqual)
        {
            // arrange

            // act
            var actualFromOperator = lhs == rhs;
            var actualFromOperatorReversed = rhs == lhs;
            var actualFromInverseOperator = lhs != rhs;
            var actualFromMethod = lhs != null ? lhs.Equals(rhs) : areExpectedToBeEqual;
            var actualFromMethodReversed = rhs != null ? rhs.Equals(lhs) : areExpectedToBeEqual;
            var actualFromObjectMethod = lhs != null ? lhs.Equals((object)rhs) : areExpectedToBeEqual;
            var actualFromObjectMethodReversed = rhs != null ? rhs.Equals((object)lhs) : areExpectedToBeEqual;

            // assert
            actualFromInverseOperator.Should().Be(!areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperator.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperatorReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");

            if ((lhs != null) && (rhs != null))
            {
                var actualFromGetHashCode = lhs.GetHashCode() == rhs.GetHashCode();

                if (areExpectedToBeEqual)
                {
                    actualFromGetHashCode.Should().BeTrue("the hashcodes should be the same on equivalent objects");
                }
            }
        }

        private static DateTime GetDate(string value)
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd", null);
        }
    }
}