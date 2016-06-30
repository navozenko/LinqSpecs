using System;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class AdHocSpecificationFixture
	{
        [Test]
        public void constructor_should_throw_exception_when_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new AdHocSpecification<string>(null));
        }

        [Test]
		public void simple_adhoc_should_work()
		{
			var specification = new AdHocSpecification<string>(n => n.StartsWith("J"));

            var result = new SampleRepository().Retrieve(specification);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
		}
	}
}