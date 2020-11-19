using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.NationalInsuranceNumberTests
{
    [TestClass]
    public class IsValidTests
    {
        [DataTestMethod]
        [DataRow("AB 12 34 56 A", true, DisplayName = "Whitespace Test 1")]
        [DataRow("AB 1 2 3 4 5 6 A", true, DisplayName = "Whitespace Test 2")]
        [DataRow("A B 1 2 3 4 5 6 A", true, DisplayName = "Whitespace Test 3")]
        [DataRow("AB 12 34 56 A ", true, DisplayName = "Whitespace Test 4")]
        [DataRow("AB123456A", true, DisplayName = "Whitespace Test 5")]
        [DataRow("AB 12 34 56", false, DisplayName = "Missing suffix")]
        [DataRow("GB 12 34 56 A", false, DisplayName = "Unallocated Prefix 1")]
        [DataRow("BG 12 34 56 A", false, DisplayName = "Unallocated Prefix 2")]
        [DataRow("NK 12 34 56 A", false, DisplayName = "Unallocated Prefix 3")]
        [DataRow("KN 12 34 56 A", false, DisplayName = "Unallocated Prefix 4")]
        [DataRow("TN 12 34 56 A", false, DisplayName = "Unallocated Prefix 5")]
        [DataRow("NT 12 34 56 A", false, DisplayName = "Unallocated Prefix 6")]
        [DataRow("ZZ 12 34 56 A", false, DisplayName = "Unallocated Prefix 7")]
        [DataRow("AO 12 34 56 A", false, DisplayName = "Invalid Second Letter 1")]
        [DataRow("AD 12 34 56 A", false, DisplayName = "Invalid Second Letter 2")]
        [DataRow("AF 12 34 56 A", false, DisplayName = "Invalid Second Letter 3")]
        [DataRow("AI 12 34 56 A", false, DisplayName = "Invalid Second Letter 4")]
        [DataRow("AQ 12 34 56 A", false, DisplayName = "Invalid Second Letter 5")]
        [DataRow("AU 12 34 56 A", false, DisplayName = "Invalid Second Letter 6")]
        [DataRow("AV 12 34 56 A", false, DisplayName = "Invalid Second Letter 7")]
        [DataRow("OA 12 34 56 A", true, DisplayName = "O is valid as first letter")]
        [DataRow("DA 12 34 56 A", false, DisplayName = "Invalid first Letter 2")]
        [DataRow("FA 12 34 56 A", false, DisplayName = "Invalid first Letter 3")]
        [DataRow("IA 12 34 56 A", false, DisplayName = "Invalid first Letter 4")]
        [DataRow("QA 12 34 56 A", false, DisplayName = "Invalid first Letter 5")]
        [DataRow("UA 12 34 56 A", false, DisplayName = "Invalid first Letter 6")]
        [DataRow("VA 12 34 56 A", false, DisplayName = "Invalid first Letter 7")]
        [DataRow("OO 12 34 56 A", false, DisplayName = "administrative prefix, but not an NI number")]
        [DataRow("FY 12 34 56 A", false, DisplayName = "temporary NI number prefix")]
        [DataRow("FY 12 34 56 A", false, DisplayName = "Attendance Allowance prefix")]
        [DataRow("Not an NI Number", false)]
        public void IsValidTest(string value, bool isValid)
        {
            // arrange

            // act
            var actual = NationalInsuranceNumber.IsValid(value);

            // assert
            actual.Should().Be(isValid, "{0} is {1}a valid National Insurance Number", value, isValid ? "" : "not ");
        }

        [TestMethod]
        public void SuffixValidationTest()
        {
            // arrange
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

            foreach (var letter in letters)
            {
                var candidate = $"AB 12 34 56 {letter}";
                var shouldBeValid = "ABCDabcd".Any(l => l == letter);

                // act
                var actual = NationalInsuranceNumber.IsValid(candidate);

                // assert
                actual.Should().Be(shouldBeValid, "{0} is {1}a valid National Insurance Number", candidate, shouldBeValid? "" : "not ");
            }

        }
    }
}
