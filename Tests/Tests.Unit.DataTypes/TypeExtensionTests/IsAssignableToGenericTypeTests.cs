using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;
using Tests.Common;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [TestClass]
    public class IsAssignableToGenericTypeTests : TestBase
    {
        [TestMethod]
        public void ConcreteOpenGenericType_ShouldBeAssignableToOpenGenericInterface()
        {
            // assert
            typeof(Foo<>).IsAssignableToGenericType(typeof(IFoo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void GenericInterface_ShouldBeAssignableToOpenGenericInterface()
        {
            typeof(IFoo<int>).IsAssignableToGenericType(typeof(IFoo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void OpenGenericInterfaceShouldBeAssignableToItself()
        {
            typeof(IFoo<>).IsAssignableToGenericType(typeof(IFoo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void ConcreteGenericClass_ShouldBeAssignableToOpenGenericInterface()
        {
            typeof(Foo<int>).IsAssignableToGenericType(typeof(IFoo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void NonGenericConcreteClassShouldBeAssignableToOpenGenericInterface()
        {
            typeof(Bar).IsAssignableToGenericType(typeof(IFoo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void NonGenericConcreteClassShouldBeAssignableToOpenGenericClass()
        {
            typeof(Pop).IsAssignableToGenericType(typeof(Foo<>))
                .Should().BeTrue();
        }

        [TestMethod]
        public void NonGenericConcreteClassShouldNotBeAssignableToUnrelatedOpenGenericInterface()
        {
            typeof(Pop).IsAssignableToGenericType(typeof(IEnumerable<>))
                .Should().BeFalse();
        }

        [TestMethod]
        public void NonGenericConcreteClassShouldNotBeAssignableToUnrelatedOpenGenericClass()
        {
            typeof(Pop).IsAssignableToGenericType(typeof(List<>))
                .Should().BeFalse();
        }

        [TestMethod]
        public void NonGenericConcreteClassShouldNotBeAssignableToUnrelatedConcreteClass()
        {
            typeof(Pop).IsAssignableToGenericType(typeof(int))
                .Should().BeFalse();
        }

        public interface IFoo<T>
        {
        }

        public class Foo<T> : IFoo<T>
        {
        }

        public class Bar : IFoo<int>
        {
        }

        public class Pop : Foo<int>
        {
        }
    }
}