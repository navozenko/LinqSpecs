using System.Collections.Generic;
using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.Tests.BooleanOperators
{
	//Note; no matter if you are using & operator, or && operator.. both works as an &&.


	[TestFixture]
	public class AndSpecificationFixture
	{
		[Test]
		public void and_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));          
			var specfication = new AndSpecification<string>(startWithJ, endsWithE);

			IEnumerable<string> result = new SampleRepository()
				.Retrieve(specfication);

			result.Satisfy(r => r.Contains("Jose")
			                    && !r.Contains("Julian")
			                    && !r.Contains("Manuel"));
		}

		[Test]
		public void and_operator_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));


			// & or && both operators behave as &&.

			IEnumerable<string> result = new SampleRepository()
				.Retrieve(startWithJ & endsWithE);

			result.Satisfy(r => r.Contains("Jose")
								&& !r.Contains("Julian")
								&& !r.Contains("Manuel"));

		}

		[Test]
		public void equals_return_true_when_both_sides_are_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var spec = startWithJ & endsWithE;

			spec.Should().Be.EqualTo(startWithJ & endsWithE);

			spec.Should("andalso is not conmutable")
				.Not.Be.EqualTo(endsWithE & startWithJ);
		}

		[Test]
		public void equals_return_false_when_both_sides_are_not_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var endsWithF = new AdHocSpecification<string>(n => n.EndsWith("f"));
			var spec = startWithJ & endsWithE;

			spec.Should().Not.Be.EqualTo(startWithJ & endsWithF);
		}
	}
}