using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class AdHocSpecificationFixture
	{
		[Test]
		public void simple_adhoc_should_work()
		{
			var specification = new AdHocSpecification<string>(n => n.StartsWith("J"));

			var result = new SampleRepository()
								.Retrieve(specification);

			result.Satisfy(r => r.Contains("Jose")
							  && r.Contains("Julian")
							  && !r.Contains("Manuel"));
		}
	}
}