using NUnit.Framework;

namespace LinqSpecs.Tests
{
    [TestFixture]
    public class AssemblyTests
    {
        [Test]
        public void Assembly_should_have_strong_name()
        {
            string? assemblyName = typeof(Specification<>).Assembly.FullName;

            Assert.That(assemblyName, Does.StartWith("LinqSpecs,"));
            Assert.That(assemblyName, Does.EndWith("PublicKeyToken=db098e6f22bae212"));
        }
    }
}
