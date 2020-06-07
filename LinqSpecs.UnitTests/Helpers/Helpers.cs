using System;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
    [TestFixture]
    public class Helpers
    {
        public static T SerializeAndDeserialize<T>(T obj)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }

        public static Expression<Func<string, bool>> NullExpression
        {
            get { return Null<Expression<Func<string, bool>>>(); }
        }

        public static Specification<string> NullSpecification
        {
            get { return Null<Specification<string>>(); }
        }

#pragma warning disable CS8603  // null
        private static T Null<T>() where T : class
        {
            return null;
        }
#pragma warning restore CS8603
    }
}
