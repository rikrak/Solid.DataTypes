using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.FactorTests
{
    [TestClass]
    public class EqualityTests
    {
        [TestMethod]
        public void SameObject()
        {
            // arrange
            var item = new Factor(1, 2);

            // act & assert
            RunEqualityTest(item, item, true);
        }

        [TestMethod]
        public void SameNumeratorAndDenominator()
        {
            // arrange
            var lhs = new Factor(1, 2);
            var rhs = new Factor(1, 2);

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void NumeratorAndDenominatorAreFactorsOfLhs()
        {
            // arrange
            var lhs = new Factor(1, 2);
            var rhs = new Factor(2, 4);

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void NumeratorAndDenominatorAreFactorsOfRhs()
        {
            // arrange
            var lhs = new Factor(3, 9);
            var rhs = new Factor(1, 3);

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void DifferentDenominators()
        {
            // arrange
            var lhs = new Factor(1, 2);
            var rhs = new Factor(1, 3);

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentNumerator()
        {
            // arrange
            var lhs = new Factor(1, 3);
            var rhs = new Factor(2, 3);

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentNumeratorAndDenominator()
        {
            // arrange
            var lhs = new Factor(1, 3);
            var rhs = new Factor(2, 9);

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void RightHandSideIsNull()
        {
            // arrange
            var lhs = new Factor(1, 3);
            Factor? rhs = null;

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        private void RunEqualityTest(Factor lhs, Factor? rhs, bool areExpectedToBeEqual)
        {
            // arrange

            // act
            var actualFromOperator = lhs == rhs;
            var actualFromOperatorReversed = rhs == lhs;
            var actualFromInverseOperator = lhs != rhs;
            var actualFromMethod = lhs.Equals(rhs);
            var actualFromMethodReversed = rhs != null ? rhs.Equals(lhs) : areExpectedToBeEqual;
            var actualFromObjectMethod = lhs.Equals((object)rhs);
            var actualFromObjectMethodReversed = rhs != null ? rhs.Equals((object)lhs) : areExpectedToBeEqual;

            // assert
            actualFromInverseOperator.Should().Be(!areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperator.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromOperatorReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");
            actualFromObjectMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal", areExpectedToBeEqual ? "" : "not ");

            if (rhs != null)
            {
                var actualFromGetHashCode = lhs.GetHashCode() == rhs.GetHashCode();

                if (areExpectedToBeEqual)
                {
                    actualFromGetHashCode.Should().BeTrue("the hashcodes should be the same on equivalent objects");
                }
            }
        }

    }
}
