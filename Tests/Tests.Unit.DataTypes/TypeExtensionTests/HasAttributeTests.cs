using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [TestClass]
    public class HasAttributeTests
    {

        [TestMethod]
        public void HasAttribute_TypeDoesNotHaveAttribute()
        {
            // arrange

            // act
            var actual = typeof(ClassWithInheritableAttribute).HasAttribute<TestClassAttribute>();

            // assert
            actual.Should().BeFalse("This class does not have the attribute");
        }

        [TestMethod]
        public void HasAttribute_TypeDoesHaveAttribute()
        {
            // arrange

            // act
            var actual = this.GetType().HasAttribute<TestClassAttribute>();

            // assert
            actual.Should().BeTrue("This class has the attribute");
        }

        [TestMethod]
        public void HasAttribute_TypeHasInheritedAttributeFromBaseClass()
        {
            // arrange
            var targetType = typeof(DerivedClassFromClassWithInheritableAttribute);
            // act
            var actual = targetType.HasAttribute<MyTestInheritableAttribute>();

            // assert
            actual.Should().BeTrue("This class has inherited the attribute from the base class");
        }

        [TestMethod]
        public void HasAttribute_DerivedTypeWithNonInheritableAttributeFromBaseClass()
        {
            // arrange
            var targetType = typeof(DerivedClassFromClassWithNonInheritableAttribute);
            // act
            var actual = targetType.HasAttribute<MyTestNonInheritableAttribute>();

            // assert
            actual.Should().BeFalse("The base class has the attribute, but it is not inherited");
        }
    }
}