using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
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
            SerializationSettings.ExpressionSerializerFactory = new FakeSerializerFactory();

            var spec = new AdHocSpecification<string>(n => n == "it works");

            var deserializedSpec = Helpers.SerializeAndDeserialize(spec);

            Assert.That(deserializedSpec, Is.InstanceOf<AdHocSpecification<string>>());
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it works"), Is.True);
            Assert.That(deserializedSpec.ToExpression().Compile().Invoke("it fails"), Is.False);
        }

        public class FakeSerializer : IExpressionSerializer
        {
            private readonly Dictionary<string, Expression> _expressions = new Dictionary<string, Expression>();

            public byte[] Serialize(Expression expression)
            {
                string text = expression.ToString();
                _expressions[text] = expression;

                using (var stream = new MemoryStream())
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(text);
                    writer.Flush();
                    stream.Flush();
                    return stream.ToArray();
                }
            }

            public Expression Deserialize(byte[] data)
            {
                using (var stream = new MemoryStream(data))
                using (var reader = new BinaryReader(stream))
                {
                    string text = reader.ReadString();
                    return _expressions[text];
                }
            }
        }

        public class FakeSerializerFactory : IExpressionSerializerFactory
        {
            private FakeSerializer _serializer = new FakeSerializer();

            public IExpressionSerializer CreateSerializer()
            {
                return _serializer;
            }
        }
    }
}