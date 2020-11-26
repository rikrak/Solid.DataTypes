using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [TestClass]
    public class GetEnumerationTypeTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetEnumerationType_TypeIsNotEnumerable_ShouldThrow()
        {
            // arrange
            var nonEnumerableType = typeof(int);

            // act & assert
            nonEnumerableType.GetEnumerationType();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsArrayOfInts_ShouldReturnInt()
        {
            RunGetEnumerationTypeTest<int[], int>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsListOfInts_ShouldReturnInt()
        {
            RunGetEnumerationTypeTest<List<int>, int>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsDictionary_ShouldReturnKeyValuePair()
        {
            RunGetEnumerationTypeTest<Dictionary<int, string>, KeyValuePair<int, string>>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsString_ShouldReturnChar()
        {
            RunGetEnumerationTypeTest<string, char>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsIListOfInts_ShouldReturnInt()
        {
            RunGetEnumerationTypeTest<IList<int>, int>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsIEnumerableOfStrings_ShouldReturnString()
        {
            RunGetEnumerationTypeTest<IEnumerable<string>, string>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsIReadOnlyCollectionOfStrings_ShouldReturnString()
        {
            RunGetEnumerationTypeTest<IReadOnlyCollection<string>, string>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsICollectionOfStrings_ShouldReturnString()
        {
            RunGetEnumerationTypeTest<ICollection<string>, string>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsIList_ShouldReturnObject()
        {
            RunGetEnumerationTypeTest<System.Collections.IList, object>();
        }

        [TestMethod]
        public void GetEnumerationType_TypeIsIEnumerable_ShouldReturnObject()
        {
            RunGetEnumerationTypeTest<System.Collections.IEnumerable, object>();
        }

        private void RunGetEnumerationTypeTest<TArray, TExpected>()
        {
            // arrange
            var arrayType = typeof(TArray);
            var expected = typeof(TExpected);

            // act
            var actual = arrayType.GetEnumerationType();

            // assert
            actual.Should().Be(expected, "The type of the array was not as expected");
        }

    }
}