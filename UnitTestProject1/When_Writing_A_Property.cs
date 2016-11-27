using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuerystringSerializer;
using System.Linq;
using QuerystringSerializer.Pairing;

namespace UnitTestProject1
{
    [TestClass]
    public class When_Writing_A_Property
    {
        public IPairer Pairer { get; set; }

        [TestInitialize]
        public void Init()
        {
            Pairer = new StandardPairer();
        }

        [TestMethod]
        public void Should_Serialize_A_String_Property()
        {
            string name = "PropertyName";
            string value = "PropertyValue";

            var result = Pairer.Pair(name, value);

            result.Should().Be("&PropertyName=PropertyValue");
        }

        [TestMethod]
        public void Should_Prepend_An_Ampersand()
        {
            string name = "PropertyName";
            string value = "PropertyValue";

            var result = Pairer.Pair(name, value);

            result.First().Should().Be('&');
        }

        [TestMethod]
        public void Should_Separate_Them_With_An_Equal()
        {

            string name = "PropertyName";
            string value = "PropertyValue";

            var result = Pairer.Pair(name, value);

            result.Split(new char[] { '=' }).First().Should().Be("&PropertyName");
            result.Split(new char[] { '=' }).Last().Should().Be("PropertyValue");
        }
    }
}
