using QuerystringSerializer.Traversing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestProject1.Models;
using System;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;
using QuerystringSerializer;

namespace UnitTestProject1
{
    [TestClass]
    public class When_Depth_First_Pre_Order_Traversal
    {
        public ITraversor Traversor { get; set; }

        [TestInitialize]
        public void Init()
        {
            Traversor = new PreorderTraversor();
        }

        [TestMethod]
        public void Should_Enumerate_Children()
        {
            var propertyValue = new Person
            {
                Name = "Alberto",
                DateOfBirth = new DateTime(1986, 12, 5),
                Dogs = new List<Dog>
                            {
                                new Dog { Name = "Alberto" }
                            },
                Partner = new Person
                {
                    Name = "A Name"
                }
            };

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(5);
        }

        [TestMethod]
        public void Should_Enumerate_Arrays()
        {
            var propertyValue = new List<Dog>
                       {
                           new Dog { Name = "Pluto" },
                           new Dog { Name = "Fuffy" }
                       };

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(2);
        }

        [TestMethod]
        public void Should_Enumerate_String_Dictionaries()
        {
            var propertyValue = new Dictionary<string, string>
            {
                { "First","Pluto" },
                { "Second","Fuffy" }
            };

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(2);
        }
    }
}
