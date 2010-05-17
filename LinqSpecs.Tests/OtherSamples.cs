using System.Collections.Generic;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;
using SharpTestsEx;
using Enumerable = System.Linq.Enumerable;

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

			result.Satisfy(r => Enumerable.Contains(r, "Jose")
			                    && !Enumerable.Contains(r, "Julian")
			                    && Enumerable.Contains(r, "Manuel"));
		}
	}
}