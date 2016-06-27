using System.Linq;
using LinqSpecs.Tests.DomainSample;
using NUnit.Framework;

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

			var result = new SampleRepository().Retrieve(specification);

            CollectionAssert.DoesNotContain(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
		}

		[Test]
		public void negate_operator_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			
			var result = new SampleRepository().Retrieve(!startWithJ);

            CollectionAssert.DoesNotContain(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
		}

		[Test]
		public void equals_return_true_when_the_negated_spec_are_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));

			var spec = !startWithJ;

            Assert.IsTrue(spec.Equals(spec));
            Assert.IsTrue(spec.Equals(!startWithJ));
		}

		[Test]
		public void equals_return_false_when_the_negated_spec_are_not_equals()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var anotherAdHocSpec = new AdHocSpecification<string>(n => n.StartsWith("dasdas"));

			var spec = !startWithJ;

            Assert.IsFalse(spec.Equals(!anotherAdHocSpec));
		}
	}
}