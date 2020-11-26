using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.EnumExtensionTests
{
    [TestClass]
    public class ToDescriptionTests
    {
        [TestMethod]
        public void EnumHasDescription()
        {
            // arrange
            const EnumWithSomeDescribed value = EnumWithSomeDescribed.ValueWithDescription;

            // act
            var actual = value.ToDescription();

            // assert
            actual.Should().Be(Resources.DescriptionValue);
        }

        [TestMethod]
        public void EnumHasNoDescription()
        {
            // arrange
            const EnumWithSomeDescribed value = EnumWithSomeDescribed.ValueWithoutDescription;

            // act
            var actual = value.ToDescription();

            // assert
            actual.Should().Be(EnumWithSomeDescribed.ValueWithoutDescription.ToString());
        }

        [TestMethod]
        public void EnumHasDisplay()
        {
            // arrange
            const EnumWithSomeDescribed value = EnumWithSomeDescribed.ValueWithDisplay;

            // act
            var actual = value.ToDescription();

            // assert
            actual.Should().Be(Resources.Resource1);
        }
    }
}