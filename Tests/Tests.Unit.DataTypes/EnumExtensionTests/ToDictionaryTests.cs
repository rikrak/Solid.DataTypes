using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.EnumExtensionTests
{
    [TestClass]
    public class ToDictionaryTests
    {
        [TestMethod]
        public void ToDictionary_HappyPath()
        {
            // arrange
            var expected = Enum.GetValues(typeof(EnumWithSomeDescribed)).Cast<EnumWithSomeDescribed>().ToArray();

            // act
            var actual = EnumExtensions.ToIntDictionary<EnumWithSomeDescribed>();

            // assert

            foreach (var e in expected)
            {
                var actualItem = actual[(int)e];
                actualItem.Should().Be(e.ToDescription());
            }

            actual.Should().HaveSameCount(expected);
        }

        [TestMethod]
        public void ToDictionary_WithFilter_HappyPath()
        {
            // arrange
            var expected = Enum.GetValues(typeof(Days))
                .Cast<Days>()
                .Where(d => d != Days.None)
                .ToArray();

            // act
            var actual = EnumExtensions.ToIntDictionary<Days>(d => d != Days.None);

            // assert

            foreach (var e in expected)
            {
                var actualItem = actual[(int)e];
                actualItem.Should().Be(e.ToDescription());
            }

            actual.Should().HaveSameCount(expected);
        }
    }
}