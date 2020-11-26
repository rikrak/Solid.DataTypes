using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.EnumExtensionTests
{
    [TestClass]
    public class GetUniqueFlagsTests
    {
        [TestMethod]
        public void GetUniqueFlags_DefaultValue()
        {
            // arrange
            Days defaultValue = default(Days);

            // act
            var actual = defaultValue.GetUniqueFlags<Days>();

            // assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUniqueFlags_ZeroValue()
        {
            // arrange
            Days value = Days.None;

            // act
            var actual = value.GetUniqueFlags<Days>();

            // assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUniqueFlags_SingleUniqueValue()
        {
            // arrange
            var value = Days.Thursday;

            // act
            var actual = value.GetUniqueFlags<Days>().ToArray();

            // assert
            actual.Should().Contain(Days.Thursday);
            actual.Should().HaveCount(1);
        }

        [TestMethod]
        public void GetUniqueFlags_ManyUniqueValues()
        {
            // arrange
            var value = Days.Thursday | Days.Saturday;

            // act
            var actual = value.GetUniqueFlags<Days>().ToArray();

            // assert
            actual.Should().Contain(Days.Thursday);
            actual.Should().Contain(Days.Saturday);
            actual.Should().HaveCount(2);
        }

        [TestMethod]
        public void GetUniqueFlags_ManyUniqueValuesThatAreSameAsAggregateValue()
        {
            // arrange
            var value = Days.Saturday | Days.Sunday;

            // act
            var actual = value.GetUniqueFlags<Days>().ToArray();

            // assert
            actual.Should().Contain(Days.Saturday);
            actual.Should().Contain(Days.Sunday);
            actual.Should().NotContain(Days.Weekend);
            actual.Should().HaveCount(2);
        }

        [TestMethod]
        public void GetUniqueFlags_ValuesAreSpecifiedByAggregate()
        {
            // arrange
            var value = Days.Weekday;

            // act
            var actual = value.GetUniqueFlags<Days>().ToArray();

            // assert
            actual.Should().Contain(Days.Monday);
            actual.Should().Contain(Days.Tuesday);
            actual.Should().Contain(Days.Wednesday);
            actual.Should().Contain(Days.Thursday);
            actual.Should().Contain(Days.Friday);
            actual.Should().NotContain(Days.Weekday);
            actual.Should().HaveCount(5);
        }
    }
}
