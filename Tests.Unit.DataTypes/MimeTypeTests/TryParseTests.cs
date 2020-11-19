using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.MimeTypeTests
{
    [TestClass]
    public class TryParseTests
    {
        [DataRow("audio/adpcm")]
        [DataRow("  audio/adpcm")]
        [DataRow("  audio/adpcm  ")]
        [DataRow("audio/adpcm  ")]
        [DataRow("audio / adpcm")]
        [DataRow("audio/ adpcm")]
        [DataRow("audio /adpcm")]
        [DataRow(" audio /adpcm ")]
        [DataRow("AUDIO/ADPCM")]
        [DataRow("Audio/Adpcm")]
        [TestMethod]
        public void ShouldConstructMimeType(string value)
        {
            // arrange

            // act
            var result = MimeType.TryParse(value, out var actual);

            // assert
            result.Should().BeTrue();
            actual.ToString().Should().Be("audio/adpcm");
        }

        [DataRow("audio/adpcm;param=val")]
        [DataRow("  audio/adpcm;param=val")]
        [DataRow("  audio/adpcm  ;param=val")]
        [DataRow("audio/adpcm  ;param=val")]
        [DataRow("audio / adpcm; param=val")]
        [DataRow("audio/ adpcm ; param=val")]
        [TestMethod]
        public void ShouldConstructMimeTypeWithParam(string value)
        {
            // arrange

            // act
            var result = MimeType.TryParse(value, out var actual);

            // assert
            result.Should().BeTrue();
            actual.ToString().Should().Be("audio/adpcm;param=val");
        }


        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("\t")]
        [TestMethod]
        public void ValueCannotBeNullOrWhitespace(string value)
        {
            // arrange

            // act
            var result = MimeType.TryParse(value, out var actual);

            // assert
            result.Should().BeFalse();
            actual.Should().Be(MimeType.None);
        }

        [DataRow("MustHaveSlash")]
        [DataRow("Too/Many/Slashes")]
        [DataRow("MustHaveSubType/")]
        [DataRow("MustHaveSubType/;")]
        [DataRow("MustHaveSubType/;param=val")]
        [DataRow("/MustHaveType")]
        [DataRow("/")]
        [DataRow(" / ")]
        [DataRow(" / ;")]
        [TestMethod]
        public void ValueShouldBeWellFormed(string value)
        {
            // arrange

            // act
            var result = MimeType.TryParse(value, out var actual);

            // assert
            result.Should().BeFalse();
            actual.Should().Be(MimeType.None);
        }

    }
}