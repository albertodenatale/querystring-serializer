using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuerystringSerializer.Traversing;
using UnitTestProject1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class When_Querying_A_Node
    {
        [TestMethod]
        public void Should_Return_True_Has_Value_When_Is_A_Primitive()
        {
            var propertyValue = 4;
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasValue();

            result.Should().Be(true);
        }

        [TestMethod]
        public void Should_Return_True_Has_Value_When_Is_A_String()
        {
            var propertyValue = "value";
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasValue();

            result.Should().Be(true);
        }

        [TestMethod]
        public void Should_Return_False_Has_Value_When_Is_A_Class()
        {
            var propertyValue = new Person();
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasValue();

            result.Should().Be(false);
        }

        [TestMethod]
        public void Should_Return_True_Has_Children_When_Contains_At_Least_One_Not_Null_Property()
        {
            var propertyValue = new { Test = 3 };
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasChildren();

            result.Should().Be(true);
        }

        [TestMethod]
        public void Should_Return_False_Has_Children_When_Not_Contains_At_Least_One_Not_Null_Property()
        {
            var propertyValue = new { };
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasChildren();

            result.Should().Be(false);
        }

        [TestMethod]
        public void Should_Return_False_Has_Children_When_A_Primitive()
        {
            var propertyValue = 3;
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasChildren();

            result.Should().Be(false);
        }

        [TestMethod]
        public void Should_Return_False_Has_Children_When_A_String()
        {
            var propertyValue = "astring";
            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.HasChildren();

            result.Should().Be(false);
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

            var propertyName = "Property";

            var node = new Node(propertyName, propertyValue);

            var result = node.Children();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(4);
        }

        [TestMethod]
        public void Should_Throw_Exception_If_Children_Requeired_And_Value_Is_Null()
        {
            var propertyName = "A name";

            var node = new Node(propertyName, null);

            Action action = () => node.Children().ToList();

            node.HasChildren().Should().BeFalse();
            node.HasValue().Should().BeFalse();
            action.ShouldThrow<InvalidOperationException>();
        }
    }
}
