using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [TestClass]
    public class IsEnumerableTests : TestBase
    {
        [TestMethod]
        public void IsEnumerable_TypeIsNotEnumerable_ShouldReturnFalse()
        {
            RunIsEnumerableTest<int>(false);
        }

        [TestMethod]
        public void IsEnumerable_TypeIsSimpleArray_ShouldReturnTrue()
        {
            RunIsEnumerableTest<int[]>(true);
        }

        [TestMethod]
        public void IsEnumerable_TypeIsSimpleEnumerable_ShouldReturnTrue()
        {
            RunIsEnumerableTest<IEnumerable<int>>(true);
        }

        [TestMethod]
        public void IsEnumerable_TypeIsList_ShouldReturnTrue()
        {
            RunIsEnumerableTest<List<int>>(true);
        }

        [TestMethod]
        public void IsEnumerable_TypeIsDictionary_ShouldReturnTrue()
        {
            RunIsEnumerableTest<Dictionary<int, string>>(true);
        }

        [TestMethod]
        public void IsEnumerable_TypeIsString_ShouldReturnTrue()
        {
            RunIsEnumerableTest<string>(true);
        }

        private void RunIsEnumerableTest<TToTest>(bool expected)
        {
            // arrange
            var type = typeof(TToTest);

            // act
            var actual = type.IsEnumerable();

            // assert
            actual.Should().Be(expected, "the type can{0} be enumerated", expected ? "" : "not");
        }
    }
}