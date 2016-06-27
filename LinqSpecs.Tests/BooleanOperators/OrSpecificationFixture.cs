using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;

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

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
		}

		[Test]
		public void or_operator_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));

			var result = new SampleRepository().Retrieve(startWithJ || endsWithN);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
		}

		[Test]
		public void equals_return_true_when_both_sides_are_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var spec = startWithJ || endsWithE;

            Assert.IsTrue(spec.Equals(spec));
            Assert.IsTrue(spec.Equals(startWithJ || endsWithE));
		}

		[Test]
		public void equals_return_false_when_both_sides_are_not_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));
			var endsWithF = new AdHocSpecification<string>(n => n.EndsWith("f"));
			var spec = startWithJ || endsWithE;

            Assert.IsFalse(spec.Equals(startWithJ || endsWithF));
            Assert.IsFalse(spec.Equals(endsWithE || startWithJ)); // OrElse is not commutable
        }
    }
}