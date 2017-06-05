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
            Assert.Throws<ArgumentNullException>(() => new AdHocSpecification<string>(null));
        }

        [Test]
		public void Simple_adhoc_should_work()
		{
			var specification = new AdHocSpecification<string>(n => n.StartsWith("J"));

            var result = new SampleRepository().Find(specification);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
		}

        [Test]
        public void Should_be_serializable()
        {
            var spec = new AdHocSpecification<string>(n => n == "it works");

            var deserializedSpec = Helpers.SerializeAndDeserialize(spec);

            Assert.That(deserializedSpec, Is.InstanceOf<AdHocSpecification<string>>());
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it works"), Is.True);
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it fails"), Is.False);
        }
    }
}