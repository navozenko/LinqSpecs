using System.IO;
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
    }
}
