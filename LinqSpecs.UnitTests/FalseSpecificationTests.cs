using System;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
    [TestFixture]
    public class FalseSpecificationTests
    {
        [Test]
        public void Should_work()
        {
            var spec = new FalseSpecification<string>();

            var result = new SampleRepository().Find(spec);

            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void Equals_returns_true_when_both_sides_are_equals()
        {
            var spec1 = new FalseSpecification<string>();
            var spec2 = new FalseSpecification<string>();

            Assert.IsTrue(spec1.Equals(spec2));
        }

        [Test]
        public void Equals_returns_false_when_both_sides_are_not_equals()
        {
            var spec = new FalseSpecification<string>();

            Assert.IsFalse(spec.Equals(10));
            Assert.IsFalse(spec.Equals(new AdHocSpecification<string>(x => true)));
            Assert.IsFalse(spec.Equals(new FalseSpecification<object>()));
            Assert.IsFalse(spec.Equals(Helpers.NullSpecification));
        }

        [Test]
        public void GetHashCode_retuns_same_value_for_equal_specifications()
        {
            var spec1 = new FalseSpecification<string>();
            var spec2 = new FalseSpecification<string>();

            Assert.AreEqual(spec1.GetHashCode(), spec2.GetHashCode());
        }

        [Test]
        public void Should_be_serializable()
        {
            var spec = new FalseSpecification<string>();

            var deserializedSpec = Helpers.SerializeAndDeserialize(spec);

            Assert.That(deserializedSpec, Is.InstanceOf<FalseSpecification<string>>());
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("any"), Is.False);
        }
    }
}
