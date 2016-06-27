using System.Collections.Generic;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class OtherSamples
	{
		[Test]
		public void combination_sample()
		{
			var startWithM = new AdHocSpecification<string>(n => n.StartsWith("M"));
			var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));

			IEnumerable<string> result = new SampleRepository()
				.Retrieve(startWithM | !endsWithN);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
		}
	}
}