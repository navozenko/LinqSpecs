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
        public static Expression<Func<string, bool>> NullExpression
        {
            get { return Null<Expression<Func<string, bool>>>(); }
        }

        public static Specification<string> NullSpecification
        {
            get { return Null<Specification<string>>(); }
        }

        private static T Null<T>() where T : class
        {
            return null!;
        }
    }
}
