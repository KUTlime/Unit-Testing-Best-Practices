// We have some class which we want to test...
namespace BusinessLayer.Models
{
    public class ClassPerMethodStructure
    {
        public string SomeProperty { get; set; } = String.Empty;

        ClassPerMethodStructure(string someProperty = String.Empty)
        {
            SomeProperty = someProperty ?? String.Empty;
        }

        public string GeneratePrefix(string prefix)
        {
            return prefix ?? String.Empty +
             SomeProperty ?? String.Empty;
        }
    }
}

// Tests should look like this
namespace BusinessLayer.Models.UnitTests
{
    public class ClassPerMethodStructureTests
    {
        [TestClass]
        public class StringConstructorTests
        {
            [TestMethod]
            public class EmptyStringConstructorTest
            {
                // Do some test logic here...
            }

            [TestMethod]
            public class NullConstructorTest
            {
                // Do some test logic here...
            }
        }

        [TestClass]
        public class GeneratePrefixMethodTests
        {
            [TestMethod]
            public class EmptyStringTest
            {
                // Do some test logic here...
            }

            [TestMethod]
            public class NullPassedTest
            {
                // Do some test logic here...
            }
        }
    }
}