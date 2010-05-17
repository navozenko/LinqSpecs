using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.Tests.BooleanOperators
{
	[TestFixture]
	public class OrSpecificationFixture
	{
		[Test]
		public void or_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));

			var result = new SampleRepository()
				.Retrieve(new OrSpecification<string>(startWithJ, endsWithN));

			result.Satisfy(r => Enumerable.Contains(r, "Jose")
			                    && Enumerable.Contains(r, "Julian")
			                    && !Enumerable.Contains(r, "Manuel"));
		}

		[Test]
		public void or_operator_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));

			var result = new SampleRepository()
				.Retrieve(startWithJ || endsWithN);

			result.Satisfy(r => r.Contains("Jose")
								&& r.Contains("Julian")
								&& !r.Contains("Manuel"));
		}

		[Test]
		public void equals_return_true_when_both_sides_are_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var spec = startWithJ || endsWithE;

			spec.Should().Be.EqualTo(startWithJ || endsWithE);

			spec.Should("orelse is not conmutable")
				.Not.Be.EqualTo(endsWithE || startWithJ);
		}

		[Test]
		public void equals_return_false_when_both_sides_are_not_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var endsWithF = new AdHocSpecification<string>(n => n.EndsWith("f"));
			var spec = startWithJ || endsWithE;

			spec.Should().Not.Be.EqualTo(startWithJ || endsWithF);
		}
	}
}