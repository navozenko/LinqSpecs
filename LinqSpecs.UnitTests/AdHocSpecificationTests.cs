using System;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
    [TestFixture]
    public class AdHocSpecificationTests
    {
        [Test]
        public void Constructor_should_throw_exception_when_argument_is_null()
        {
            Assert.Throws<ArgumentNullException>(() => new AdHocSpecification<string>(null!));
        }

        [Test]
        public void Predicate_should_work()
        {
            var specification = new AdHocSpecification<string>(n => n.StartsWith("J"));

            var result = new SampleRepository().Find(specification);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
        }

        [Test]
        public void Equals_returns_true_when_another_specification_has_identical_predicate()
        {
            var spec1 = new AdHocSpecification<string>(x => x.Length > 1 && x.StartsWith("J"));
            var spec2 = new AdHocSpecification<string>(x => x.Length > 1 && x.StartsWith("J"));

            Assert.IsTrue(spec1.Equals(spec1));
            Assert.IsTrue(spec1.Equals(spec2));
        }

        [Test]
        public void Equals_returns_false_when_another_specification_has_different_predicate()
        {
            var spec1 = new AdHocSpecification<string>(x => x.Length > 1);
            var spec2 = new AdHocSpecification<string>(x => x.Length > 2);

            Assert.IsFalse(spec1.Equals(spec2));
        }

        [Test]
        public void Equals_returns_false_when_other_specification_has_different_type()
        {
            var spec = new AdHocSpecification<string>(x => true);

            Assert.IsFalse(spec.Equals(10));
            Assert.IsFalse(spec.Equals(new AdHocSpecification<int>(x => true)));
            Assert.IsFalse(spec.Equals(new AdHocSpecification<object>(x => true)));
            Assert.IsFalse(spec.Equals(new TrueSpecification<string>()));
            Assert.IsFalse(spec.Equals(null!));
        }

        [Test]
        public void GetHashCode_retuns_same_value_for_equal_specifications()
        {
            var spec1 = new AdHocSpecification<string>(x => x.Length > 1);
            var spec2 = new AdHocSpecification<string>(x => x.Length > 1);

            Assert.AreEqual(spec1.GetHashCode(), spec2.GetHashCode());
        }
    }
}
