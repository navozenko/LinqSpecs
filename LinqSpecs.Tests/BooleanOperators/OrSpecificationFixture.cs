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
        public void Equals_returns_true_when_both_sides_are_equals()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var spec = s1 | s2;

            Assert.IsInstanceOf<OrSpecification<string>>(spec);
            Assert.IsTrue(spec.Equals(spec));
            Assert.IsTrue(spec.Equals(s1 | s2));
            Assert.IsTrue(spec.Equals(s1 || s2)); // | or || both operators behave as ||
        }

        [Test]
        public void Equals_returns_false_when_both_sides_are_not_equals()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var s3 = new AdHocSpecification<string>(x => x.Length > 3);
            var spec = s1 | s2;

            Assert.IsInstanceOf<OrSpecification<string>>(spec);
            Assert.IsFalse(spec.Equals(null));
            Assert.IsFalse(spec.Equals(10));
            Assert.IsFalse(spec.Equals(s1));
            Assert.IsFalse(spec.Equals(s2));
            Assert.IsFalse(spec.Equals(s2 | s1)); // OrElse is not commutable
            Assert.IsFalse(spec.Equals(s1 | s3));
            Assert.IsFalse(spec.Equals(s3 | s2));
        }

        [Test]
        public void GetHashCode_retuns_same_value_for_equal_specifications()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var s3 = new AdHocSpecification<string>(x => x.Length > 3);
            var spec1 = s1 | s2 | s3;
            var spec2 = s1 | s2 | s3;

            Assert.IsInstanceOf<OrSpecification<string>>(spec1);
            Assert.IsInstanceOf<OrSpecification<string>>(spec2);
            Assert.AreEqual(spec1.GetHashCode(), spec2.GetHashCode());
        }
    }
}