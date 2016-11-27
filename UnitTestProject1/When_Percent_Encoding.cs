using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuerystringSerializer.Encoding;

namespace UnitTestProject1
{
    [TestClass]
    public class When_Percent_Encoding
    {
        public IEncoder Encoder { get; set; }

        [TestInitialize]
        public void Init()
        {
            Encoder = new StandardEncoder();
        }

        [TestMethod]
        public void Should_Encode_An_Exclamation_Mark()
        {
            string input = "!";

            var result = Encoder.Encode(input);

            result.Should().Be("%21");
        }

        [TestMethod]
        public void Should_Encode_A_Number_Sign()
        {
            string input = "#";

            var result = Encoder.Encode(input);

            result.Should().Be("%23");
        }

        [TestMethod]
        public void Should_Encode_A_Dollar_Sign()
        {
            string input = "$";

            var result = Encoder.Encode(input);

            result.Should().Be("%24");
        }

        [TestMethod]
        public void Should_Encode_An_Ampersand()
        {
            string input = "&";

            var result = Encoder.Encode(input);

            result.Should().Be("%26");
        }

        [TestMethod]
        public void Should_Encode_An_Apostrophe()
        {
            string input = "'";

            var result = Encoder.Encode(input);

            result.Should().Be("%27");
        }

        [TestMethod]
        public void Should_Encode_An_Opening_Round_Parentheses()
        {
            string input = "(";

            var result = Encoder.Encode(input);

            result.Should().Be("%28");
        }

        [TestMethod]
        public void Should_Encode_An_Closing_Round_Parentheses()
        {
            string input = ")";

            var result = Encoder.Encode(input);

            result.Should().Be("%29");
        }

        [TestMethod]
        public void Should_Encode_An_Asterisk()
        {
            string input = "*";

            var result = Encoder.Encode(input);

            result.Should().Be("%2A");
        }

        [TestMethod]
        public void Should_Encode_A_Plus()
        {
            string input = "+";

            var result = Encoder.Encode(input);

            result.Should().Be("%2B");
        }

        [TestMethod]
        public void Should_Encode_A_Comma()
        {
            string input = ",";

            var result = Encoder.Encode(input);

            result.Should().Be("%2C");
        }

        [TestMethod]
        public void Should_Encode_A_Slash()
        {
            string input = "/";

            var result = Encoder.Encode(input);

            result.Should().Be("%2F");
        }

        [TestMethod]
        public void Should_Encode_A_Colon()
        {
            string input = ":";

            var result = Encoder.Encode(input);

            result.Should().Be("%3A");
        }

        [TestMethod]
        public void Should_Encode_A_Semi_Colon()
        {
            string input = ";";

            var result = Encoder.Encode(input);

            result.Should().Be("%3B");
        }

        [TestMethod]
        public void Should_Encode_An_Equal()
        {
            string input = "=";

            var result = Encoder.Encode(input);

            result.Should().Be("%3D");
        }

        [TestMethod]
        public void Should_Encode_A_Question_Mark()
        {
            string input = "?";

            var result = Encoder.Encode(input);

            result.Should().Be("%3F");
        }

        [TestMethod]
        public void Should_Encode_An_At()
        {
            string input = "@";

            var result = Encoder.Encode(input);

            result.Should().Be("%40");
        }

        [TestMethod]
        public void Should_Encode_An_Opening_Square_Bracket()
        {
            string input = "[";

            var result = Encoder.Encode(input);

            result.Should().Be("%5B");
        }

        [TestMethod]
        public void Should_Encode_A_Closing_Square_Bracket()
        {
            string input = "]";

            var result = Encoder.Encode(input);

            result.Should().Be("%5D");
        }

        [TestMethod]
        public void Should_Throw_An_Exception_When_Empty_Input()
        {
            string input = string.Empty;

            Action action  = () => Encoder.Encode(input);

            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void Should_Throw_An_Exception_When_Null_Input()
        {
            string input = null;

            Action action = () => Encoder.Encode(input);

            action.ShouldThrow<ArgumentNullException>();
        }
    }
}
