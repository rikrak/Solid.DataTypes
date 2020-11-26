using System;

namespace Tests.Unit.DataTypes.TypeExtensionTests
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class MyTestInheritableAttribute : Attribute { }
    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class MyTestNonInheritableAttribute : Attribute { }

    [MyTestInheritable]
    public class ClassWithInheritableAttribute { }

    [MyTestNonInheritable]
    public class ClassWithNonInheritableAttribute { }

    public class DerivedClassFromClassWithInheritableAttribute : ClassWithInheritableAttribute { }
    public class DerivedClassFromClassWithNonInheritableAttribute : ClassWithNonInheritableAttribute { }

}
