using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class AdHocSpecificationFixture
	{
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