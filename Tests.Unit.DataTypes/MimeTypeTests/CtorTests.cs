using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.MimeTypeTests
{
    [TestClass]
    public class CtorTests
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
            var actual = new MimeType(value);

            // assert
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
            var actual = new MimeType(value);

            // assert
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
            Action act = () => new MimeType(value);

            // assert
            act.Should().Throw<ArgumentException>();
        }

        [DataRow("MustHaveSlash")]
        [DataRow("Too/Many/Slashes")]
        [DataRow("MustHaveSubType/")]
        [DataRow("MustHaveSubType/;param=val")]
        [DataRow("/MustHaveType")]
        [DataRow("/")]
        [DataRow("/;")]
        [DataRow(" / ")]
        [DataRow(" / ;")]
        [TestMethod]
        public void ValueShouldBeWellFormed(string value)
        {
            // arrange

            // act
            Action act = () => new MimeType(value);

            // assert
            act.Should().Throw<ArgumentException>();
        }

    }
}
