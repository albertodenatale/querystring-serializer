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
                Name = "APerson",
                DateOfBirth = new DateTime(1986, 12, 5),
                Dogs = new List<Dog>
                            {
                                new Dog { Name = "Fuffy" }
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
            result.Count().Should().Be(7);
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
        public void Should_Enumerate_Dictionaries()
        {
            var propertyValue = new Dictionary<string, object>
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

        [TestMethod]
        public void Should_Throw_An_Exception_If_Dictionary_Key_Is_Not_String()
        {
            var propertyValue = new Dictionary<object, string>
            {
                { new object(),"Pluto" },
                { "Second","Fuffy" }
            };

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            Action action = () => Traversor.GetPairs().ToList();

            action.ShouldThrow<ArgumentException>("Only string keys can be contained");
        }

        [TestMethod]
        public void Should_Enumerate_Anonymous()
        {
            var propertyValue = new
            {
                Name = "APerson",
                DateOfBirth = new DateTime(1986, 12, 5),
                Dogs = new []
                        {
                            new { Name = "Fuffy" }
                        },
                Partner = new
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
            result.Count().Should().Be(4);
        }

        [TestMethod]
        public void Should_Handle_Chars()
        {
            var propertyValue = new
            {
                Name = 'A'
            };

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
            result.First().Value.Should().Be('A');
        }

        [TestMethod]
        public void Should_Handle_Nullable_Chars()
        {
            char? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
            result.First().Value.Should().Be(null);
        }

        [TestMethod]
        public void Should_Handle_Bools()
        {
            bool propertyValue = true;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
            result.First().Value.Should().Be(true);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Bools()
        {
            bool? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);
            result.First().Value.Should().Be(null);
        }

        [TestMethod]
        public void Should_Handle_SBytes()
        {
            sbyte propertyValue = -128;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var sByte = result.First().Value as sbyte?;

            sByte.HasValue.Should().Be(true);
            sByte.Value.Should().Be(-128);
        }

        [TestMethod]
        public void Should_Handle_Nullable_SBytes()
        {
            sbyte? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var sByte = result.First().Value as sbyte?;

            sByte.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_Shorts()
        {
            short propertyValue = 32767;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as short?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(32767);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Shorts()
        {
            short? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as short?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_UShorts()
        {
            ushort propertyValue = 65535;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as ushort?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(65535);
        }

        [TestMethod]
        public void Should_Handle_Nullable_UShorts()
        {
            ushort? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as ushort?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_Bytes()
        {
            byte propertyValue = 255;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as byte?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(255);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Bytes()
        {
            byte? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as byte?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_UInt()
        {
            uint propertyValue = 4294967295U;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as uint?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(4294967295U);
        }

        [TestMethod]
        public void Should_Handle_Nullable_UInt()
        {
            uint? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as uint?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_Longs()
        {
            long propertyValue = 9223372036854775807L;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as long?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(9223372036854775807L);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Longs()
        {
            long? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as long?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_ULongs()
        {
            ulong propertyValue = 18446744073709551615UL;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as ulong?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(18446744073709551615UL);
        }

        [TestMethod]
        public void Should_Handle_Nullable_ULongs()
        {
            ulong? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as ulong?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_Floats()
        {
            float propertyValue = 3.5f;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as float?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(3.5f);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Floats()
        {
            float? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as float?;

            number.HasValue.Should().Be(false);
        }

        [TestMethod]
        public void Should_Handle_Doubles()
        {
            double propertyValue = 100.5D;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as double?;

            number.HasValue.Should().Be(true);
            number.Value.Should().Be(100.5D);
        }

        [TestMethod]
        public void Should_Handle_Nullable_Doubles()
        {
            float? propertyValue = null;

            Traversor.Tree = new Tree
            {
                Root = new Node("ROOT", propertyValue)
            };

            var result = Traversor.GetPairs().ToList();

            result.Should().NotBeEmpty();
            result.Count().Should().Be(1);

            var number = result.First().Value as float?;

            number.HasValue.Should().Be(false);
        }
    }
}
