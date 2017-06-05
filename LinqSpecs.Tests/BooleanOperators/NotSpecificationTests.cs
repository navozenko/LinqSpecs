using System;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class NotSpecificationTests
	{
        [Test]
        public void Constructor_should_throw_exception_when_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new NotSpecification<string>(null));
        }

        [Test]
		public void Negate_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var specification = new NotSpecification<string>(startWithJ);

			var result = new SampleRepository().Find(specification);

            CollectionAssert.DoesNotContain(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
		}

        [Test]
        public void Equals_return_true_when_the_negated_spec_are_equals()
        {
            var sourceSpec = new AdHocSpecification<string>(x => x.Length > 1);
            var spec = !sourceSpec;

            Assert.IsInstanceOf<NotSpecification<string>>(spec);
            Assert.IsTrue(spec.Equals(spec));
            Assert.IsTrue(spec.Equals(!sourceSpec));
        }

        [Test]
        public void Equals_return_false_when_the_negated_spec_are_not_equals()
        {
            var sourceSpec1 = new AdHocSpecification<string>(x => x.Length > 1);
            var sourceSpec2 = new AdHocSpecification<string>(x => x.Length > 2);
            var spec = !sourceSpec1;

            Assert.IsInstanceOf<NotSpecification<string>>(spec);
            Assert.IsFalse(spec.Equals(null));
            Assert.IsFalse(spec.Equals(10));
            Assert.IsFalse(spec.Equals(sourceSpec1));
            Assert.IsFalse(spec.Equals(sourceSpec2));
            Assert.IsFalse(spec.Equals(!sourceSpec2));
        }

        [Test]
        public void GetHashCode_retuns_same_value_for_equal_specifications()
        {
            var sourceSpec = new AdHocSpecification<string>(x => x.Length > 0);
            var spec1 = !sourceSpec;
            var spec2 = !sourceSpec;

            Assert.IsInstanceOf<NotSpecification<string>>(spec1);
            Assert.IsInstanceOf<NotSpecification<string>>(spec2);
            Assert.AreEqual(spec1.GetHashCode(), spec2.GetHashCode());
        }

        [Test]
        public void Should_be_serializable()
        {
            var sourceSpec = new AdHocSpecification<string>(n => n == "it fails");
            var spec = new NotSpecification<string>(sourceSpec);

            var deserializedSpec = Helpers.SerializeAndDeserialize(spec);

            Assert.That(deserializedSpec, Is.InstanceOf<NotSpecification<string>>());
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it works"), Is.True);
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it fails"), Is.False);
        }
    }
}