using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.Tests.BooleanOperators
{
	[TestFixture]
	public class NegateSpecificationFixture
	{
		[Test]
		public void negate_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var specification = new NegateSpecification<string>(startWithJ);

			var result = new SampleRepository()
				.Retrieve(specification);

			result.Satisfy(r => !r.Contains("Jose")
								&& !r.Contains("Julian")
								&& r.Contains("Manuel"));
		}

		[Test]
		public void negate_operator_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			
			var result = new SampleRepository()
				.Retrieve(!startWithJ);

			result.Satisfy(r => !r.Contains("Jose")
								&& !r.Contains("Julian")
								&& r.Contains("Manuel"));
		}

		[Test]
		public void equals_return_true_when_the_negated_spec_are_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));

			var spec = !startWithJ;

			spec.Should().Be.EqualTo(!startWithJ);

		}

		[Test]
		public void equals_return_false_when_the_negated_spec_are_not_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var anotherAdHocSpec = new AdHocSpecification<string>(n => n.StartsWith("dasdas"));

			var spec = !startWithJ;
			
			spec.Should().Not.Be.EqualTo(!anotherAdHocSpec);

		}
	}
}