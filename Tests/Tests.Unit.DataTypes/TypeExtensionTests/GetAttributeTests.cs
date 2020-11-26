using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solid.DataTypes;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [TestClass]
    public class GetAttributeTests
    {

        [TestMethod]
        public void GetAttribute_TypeDoesNotHaveAttribute()
        {
            // arrange

            // act
            var actual = typeof(ClassWithInheritableAttribute).GetAttribute<TestClassAttribute>();

            // assert
            actual.Should().BeNull("This class does not have the attribute");
        }

        [TestMethod]
        public void GetAttribute_TypeDoesHaveAttribute()
        {
            // arrange

            // act
            var actual = this.GetType().GetAttribute<TestClassAttribute>();

            // assert
            actual.Should().NotBeNull("This class has the attribute");
        }

        [TestMethod]
        public void GetAttribute_TypeHasInheritedAttributeFromBaseClass()
        {
            // arrange
            var targetType = typeof(DerivedClassFromClassWithInheritableAttribute);
            // act
            var actual = targetType.GetAttribute<MyTestInheritableAttribute>();

            // assert
            actual.Should().NotBeNull("This class has inherited the attribute from the base class");
        }

        [TestMethod]
        public void GetAttribute_DerivedTypeWithNonInheritableAttributeFromBaseClass()
        {
            // arrange
            var targetType = typeof(DerivedClassFromClassWithNonInheritableAttribute);
            // act
            var actual = targetType.GetAttribute<MyTestNonInheritableAttribute>();

            // assert
            actual.Should().BeNull("The base class has the attribute, but it is not inherited");
        }

    }
}