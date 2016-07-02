using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	//Note; no matter if you are using & operator, or && operator.. both works as an &&.

	[TestFixture]
	public class AndSpecificationFixture
	{
        [Test]
        public void constructor_should_throw_exception_when_argument_is_null()
        {
            var spec = new AdHocSpecification<string>(x => x.Length == 1);

            Assert.Throws<ArgumentNullException>(() => new AndSpecification<string>(spec, null));
            Assert.Throws<ArgumentNullException>(() => new AndSpecification<string>(null, spec));
        }

        [Test]
		public void and_should_work()
		{
			var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
			var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));          
			var specfication = new AndSpecification<string>(startWithJ, endsWithE);

			IEnumerable<string> result = new SampleRepository().Retrieve(specfication);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
		}

        [Test]
        public void Equals_returns_true_when_both_sides_are_equals()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var spec = s1 & s2;

            Assert.IsInstanceOf<AndSpecification<string>>(spec);
            Assert.IsTrue(spec.Equals(spec));
            Assert.IsTrue(spec.Equals(s1 & s2));
            Assert.IsTrue(spec.Equals(s1 && s2)); // & or && both operators behave as &&
        }

        [Test]
        public void Equals_returns_false_when_both_sides_are_not_equals()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var s3 = new AdHocSpecification<string>(x => x.Length > 3);
            var spec = s1 & s2;

            Assert.IsInstanceOf<AndSpecification<string>>(spec);
            Assert.IsFalse(spec.Equals(null));
            Assert.IsFalse(spec.Equals(10));
            Assert.IsFalse(spec.Equals(s1));
            Assert.IsFalse(spec.Equals(s2));
            Assert.IsFalse(spec.Equals(s2 & s1)); // AndAlso is not commutable
            Assert.IsFalse(spec.Equals(s1 & s3));
            Assert.IsFalse(spec.Equals(s3 & s2));
        }

        [Test]
        public void GetHashCode_retuns_same_value_for_equal_specifications()
        {
            var s1 = new AdHocSpecification<string>(x => x.Length > 1);
            var s2 = new AdHocSpecification<string>(x => x.Length > 2);
            var s3 = new AdHocSpecification<string>(x => x.Length > 3);
            var spec1 = s1 & s2 & s3;
            var spec2 = s1 & s2 & s3;

            Assert.IsInstanceOf<AndSpecification<string>>(spec1);
            Assert.IsInstanceOf<AndSpecification<string>>(spec2);
            Assert.AreEqual(spec1.GetHashCode(), spec2.GetHashCode());
        }

        [Test]
        public void should_be_serializable()
        {
            var sourceSpec1 = new AdHocSpecification<string>(n => n.StartsWith("it"));
            var sourceSpec2 = new AdHocSpecification<string>(n => n.EndsWith("works"));
            var spec = new AndSpecification<string>(sourceSpec1, sourceSpec2);

            var deserializedSpec = Helpers.SerializeAndDeserialize(spec);

            Assert.That(deserializedSpec, Is.InstanceOf<AndSpecification<string>>());
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it works"), Is.True);
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it fails"), Is.False);
        }
    }
}