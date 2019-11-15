# Unit Testing Best Practises
A collection of best practises &amp; tools for Unit Testing related to .NET.

# Using some boiler plate code generator
See **Tools** section for details.

# Using a structure for unit tests
Over time, I've used several different types of unit test structures for UT organization. The best structure which I'm using since the beginning of 2018 is listed below. The original article is listed in Links session.
```csharp
// Testing scheme
namespace OriginalClassNameSpaceUnitTests
{
    public class ClassNameTests
    {
        [TestClass]
        public class SomeMethodTests
        {
            [TestMethod]
            public class SomeFunctionalityOfMethodTest
            {
                // Do some test logic here...
            }

        }

        [TestClass]
        public class SomeOtherMethodTests
        {
            [TestMethod]
            public class SomeFunctionalityOfMethodTest
            {
                // Do some test logic here...
            }

        }
    }
}
```

# Using test categories
Using categories in unit testing is well excepted practise, sometime poorly implemented.
```csharp
[TestCategory("Basic tests")]
[TestMethod]
public void SomeConstructorOrMethodTest()
{
    // Arrange
    // Act
    // Assert
}
```
Instead of using:
```csharp
[TestCategory("Basic tests")]
[TestMethod]
public void SomeConstructorOrMethodTest()
{
    // Arrange
    // Act
    // Assert
}
```
use
```csharp
[TestCategory(TestCategory.BasicTests)]
[TestMethod]
public void SomeConstructorOrMethodTest()
{
    // Arrange
    // Act
    // Assert
}

namespace OriginalNameSpace.UnitTests
{
    /// <summary>
    /// Provides the test category strings used in attributes.
    /// </summary>
    /// <remarks>Members of this class should be used in unit test attributes.</remarks>
    public static class TestCategory
    {
        /// <summary>
        /// Provides a string name for a basic unit test category.
        /// </summary>
        /// <remarks>All basic or data driven tests should use this category.</remarks>
        public const string BasicTests = "Basic tests";

        /// <summary>
        /// Provides a string name for an integration test category.
        /// </summary>
        /// <remarks>All test which qualifies like an integration test should use this category.</remarks>
        public const string IntegrationTests = "Integration tests";
    }
}
```

# Tools
## Boiler Plate Unit Test Generator
A free Visual Studio extension which generates a boiler plate code for tests. Adds a new solution and structure to existing solution based on the structure of existing code. 

You have to add unit test project **manually** for .NET Core projects with this extension.

### Boiler Plate Unit Test Generator Setup for Visual Studio
This is a setup for Visual Studio Boiler Plate Unit Test Generator which ensures the scheme proposed in above. To set it up go to Tools -> Options -> Boiler Plate Unit Test Generator -> Test File Content

This goes to the first edit box.
```csharp
$UsingStatements$

namespace $Namespace$
{
	public class $ClassName$Tests
	{
		$TestMethods$
	}
}
```

This is a setup for the Test method name format - Invocation.
```csharp
[TestClass]
public class $TestMethodName$Tests
{
	[TestMethod]
	public $AsyncModifier$ $AsyncReturnType$ $TestMethodName$Test()
	{
		// Arrange
		var $ClassNameShort.CamelCase$ = $TodoConstructor$;
		$ParameterSetupDefaults.NewLineIfPopulated$	
		// Act
		$MethodInvocationPrefix$$ClassNameShort.CamelCase$$MethodInvocation$;
	
		// Assert
		Assert.Fail();
	}
}
```

This is a setup for the Test method name format - Empty.
```csharp
[TestClass]
public void $TestMethodName$Tests()
{
	[TestMethod]
	public void $TestMethodName$Test()
	{
		// Arrange

	
		// Act
	
	
		// Assert
		Assert.Fail();
	}
}

```

## Snippets to Visual Studio (MSTest)
Use Visual Studio Code Snippet Manager to create these snippets:

A **testclass** snippet to generate a testing class for all other tests:
```csharp
public class $className$Tests
{
    [TestClass]
    public class $className$ConstructorTests
    {
        [TestMethod]
        public void EmptyConstructorTest()
        {
            // Arrange

            // Act

            // Assert
            Assert.Fail();
        }
    }
}
```

A **testmethod** snippet to generate the method testing stub within the testing class:
```csharp
[TestClass]
public class $MethodToTest$Tests
{
    [TestMethod]
    public void $MethodToTest$Test()
    {
        // Arrange

        // Act

        // Assert
        Assert.Fail();
    }
}
```

A **test** snippet to generate a simple test within a testing class:
```csharp
[TestMethod]
public void $MethodToTest$Test()
{
    // Arrange

    // Act

    // Assert
    Assert.Fail();
}
```

# FAQ
## How to test private methods?
That is a tricky question. Generally, testing of private methods is not recommended mostly because OOP hacking but Microsoft provides a solution for this problem. One approach is to use [PrivateObject Class](https://docs.microsoft.com/en-us/dotnet/api/microsoft.visualstudio.testtools.unittesting.privateobject). See discussion at SO in **Links** to make your own decision.


```csharp
Class target = new Class();
PrivateObject obj = new PrivateObject(target);
var retVal = obj.Invoke("PrivateMethod");
Assert.AreEqual(expectedVal, retVal);
```

## How to test protected methods?
This is quite simple:
```csharp
[TestClass]
public class Test1 : SomeClass
{
    [TestMethod]
    public void MyTest
    {
        Assert.AreEqual(1, ProtectedMethod());
    }

}
```
or use an attribute to decorate the class.
```csharp
[assembly: InternalsVisibleTo("TestsAssembly")]
```

## How to organize the structure of UT project?
The way is to organize it **exactly** same as the original project or projects. Every business project should have its own dedicated UT project labelled with "**.UnitTests**". It helps with navigation and orientation within complicated projects.

*Example:*<br>
Business project:&nbsp;&nbsp;&nbsp;**AgentManager.Worker**<br>
UT project:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**AgentManager.Worker.UnitTests**

# Links
[Structuring Unit Tests](https://haacked.com/archive/2012/01/02/structuring-unit-tests.aspx/) (the best article about structure of unit tests which I've read)<br>
[Unit Test Boilerplate Generator GitHub](https://github.com/microsoft/UnitTestBoilerplateGenerator)<br>
[Unit Test Boilerplate Generator MarketPlace](https://marketplace.visualstudio.com/items?itemName=RandomEngy.UnitTestBoilerplateGenerator)<br>
[Unit Testing private methods in C#](https://stackoverflow.com/questions/9122708/unit-testing-private-methods-in-c-sharp)<br>

