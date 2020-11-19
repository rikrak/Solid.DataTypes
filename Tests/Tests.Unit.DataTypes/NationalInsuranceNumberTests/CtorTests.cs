using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.NationalInsuranceNumberTests
{
    [TestClass]
    public class CtorTests
    {
        [DataRow("", DisplayName = "Empty Value")]
        [DataRow(null, DisplayName = "null Value")]
        [DataRow(" \t", DisplayName = "whitespace Value")]
        [DataRow("Not an NI", DisplayName = "invalid Value")]
        [DataTestMethod]
        public void CannotCreateInvalidNationalInsuranceNumber(string value)
        {
            // arrange

            // act
            Action act = () => new NationalInsuranceNumber(value);

            // assert
            act.Should().Throw<Exception>();
        }
    }
}