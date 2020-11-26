using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
