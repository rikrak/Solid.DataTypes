using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.MimeTypeTests
{
    [TestClass]
    public class EqualityTest
    {
        [TestMethod]
        public void SameValue()
        {
            // arrange
            var lhs = new MimeType("application/xml");
            var rhs = new MimeType("application/xml");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void SameValueWithParam()
        {
            // arrange
            var lhs = new MimeType("application/xml;charset=utf-8");
            var rhs = new MimeType("application/xml;charset=utf-8");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void SameValueDifferentCase()
        {
            // arrange
            var lhs = new MimeType("application/xml");
            var rhs = new MimeType("Application/XML");

            // act & assert
            RunEqualityTest(lhs, rhs, true);
        }

        [TestMethod]
        public void DifferentType()
        {
            // arrange
            var lhs = new MimeType("text/plain");
            var rhs = new MimeType("crisps/plain");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentSubType()
        {
            // arrange
            var lhs = new MimeType("application/xml");
            var rhs = new MimeType("application/zip");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void SameValueDifferentParameter()
        {
            // arrange
            var lhs = new MimeType("text/plain;charset=ascii");
            var rhs = new MimeType("test/plain;charset=utf-8");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        [TestMethod]
        public void DifferentValue()
        {
            // arrange
            var lhs = new MimeType("application/xml");
            var rhs = new MimeType("application/octet-stream");

            // act & assert
            RunEqualityTest(lhs, rhs, false);
        }

        private void RunEqualityTest(in MimeType lhs, in MimeType rhs, bool areExpectedToBeEqual, string because = null)
        {
            // arrange

            // act
            var actualFromOperator = lhs == rhs;
            var actualFromOperatorReversed = rhs == lhs;
            var actualFromInverseOperator = lhs != rhs;
            var actualFromInverseOperatorReversed = rhs != lhs;
            var actualFromMethod = lhs.Equals(rhs);
            var actualFromMethodReversed = rhs.Equals(lhs);
            var lhsHash = lhs.GetHashCode();
            var rhsHash = rhs.GetHashCode();

            // assert
            actualFromInverseOperator.Should().Be(!areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);
            actualFromInverseOperatorReversed.Should().Be(!areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);
            actualFromOperator.Should().Be(areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);
            actualFromOperatorReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);
            actualFromMethod.Should().Be(areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);
            actualFromMethodReversed.Should().Be(areExpectedToBeEqual, "The instances are {0}equal {1}", areExpectedToBeEqual ? "" : "not ", because);

            if (areExpectedToBeEqual)
            {
                lhs.Should().Be(rhs);
                rhs.Should().Be(lhs);
                lhsHash.Should().Be(rhsHash);
            }
            else
            {
                lhs.Should().NotBe(rhs);
                rhs.Should().NotBe(lhs);
                lhsHash.Should().NotBe(rhsHash);
            }
        }
    }
}