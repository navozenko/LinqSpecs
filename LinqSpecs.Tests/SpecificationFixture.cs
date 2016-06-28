using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class SpecificationFixture
	{
        [Test]
        public void can_implicitly_convert_specification_to_expression()
        {
            Specification<string> spec = new AdHocSpecification<string>(s => s.Length == 2);
            Expression<Func<string, bool>> expr = spec;

            Assert.IsTrue(expr.Compile().Invoke("ab"));
            Assert.IsFalse(expr.Compile().Invoke("abcd"));
        }
    }
}