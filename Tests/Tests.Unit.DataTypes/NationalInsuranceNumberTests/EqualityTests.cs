using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.NationalInsuranceNumberTests
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void SameObject()
        {
            // arrange
            var item = new NationalInsuranceNumber("AB 12 34 56 D");

            // act & assert
            RunEqualityTest(item, item, true);
        }

        [TestMethod]
        public void SameValue()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("AB 12 34 56 D");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void SameValueDifferentCase()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("ab 12 34 56 d");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void SameValueDifferentWhitespace()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("AB123456D");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void SameValueDifferentWhitespaceAndCase()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("ab123456d");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void DifferentSuffix()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("AB 12 34 56 C");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentPrefix()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("AE 12 34 56 D");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentNumbers()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB 12 34 56 D");
            var rhs = new NationalInsuranceNumber("AB 21 34 56 D");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void ObjectCompareToNull()
        {
            // arrange
            var lhs = new NationalInsuranceNumber("AB123456C");

            // act
            var actual = lhs.Equals(null);

            // assert
            actual.Should().BeFalse();
        }

        private void RunEqualityTest(NationalInsuranceNumber lhs, NationalInsuranceNumber rhs, bool areExpectedToBeEqual)
        {
            // arrange

            // act
            var actualFromOperator = lhs == rhs;
            var actualFromOperatorReversed = rhs == lhs;
            var actualFromInverseOperator = lhs != rhs;
            var actualFromMethod = lhs.Equals(rhs);
            var actualFromMethodReversed = rhs.Equals(lhs);
            var actualFromObjectMethod = lhs.Equals((object)rhs);
            var actualFromObjectMethodReversed = rhs.Equals((object)lhs);

            // assert
            actualFromInverseOperator.Should().Be(!areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperator.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperatorReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");

            var actualFromGetHashCode = lhs.GetHashCode() == rhs.GetHashCode();

            if (areExpectedToBeEqual)
            {
                actualFromGetHashCode.Should().BeTrue("the hashcodes should be the same on equivalent objects");
            }
        }
    }
}