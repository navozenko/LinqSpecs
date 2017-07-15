using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace LinqSpecs.Tests
{
	[TestFixture]
	public class SpecificationTests
	{
        [Test]
        public void ToString_should_return_expression_string()
        {
            Expression<Func<string, bool>> expr = s => s.Length == 2;
            Specification<string> spec = new AdHocSpecification<string>(expr);

            Assert.AreEqual(expr.ToString(), spec.ToString());
        }

        [Test]
        public void Can_implicitly_convert_specification_to_expression()
        {
            Specification<string> spec = new AdHocSpecification<string>(s => s.Length == 2);
            Expression<Func<string, bool>> expr = spec;

            Assert.IsTrue(expr.Compile().Invoke("ab"));
            Assert.IsFalse(expr.Compile().Invoke("abcd"));
        }

        [Test]
        public void And_operator_should_work()
        {
            var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
            var endsWithE = new AdHocSpecification<string>(n => n.EndsWith("e"));

            var result = new SampleRepository().Find(startWithJ & endsWithE);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
        }

        [Test]
        public void Or_operator_should_work()
        {
            var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));
            var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));

            var result = new SampleRepository().Find(startWithJ | endsWithN);

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.Contains(result, "Julian");
            CollectionAssert.DoesNotContain(result, "Manuel");
        }

        [Test]
        public void Negate_operator_should_work()
        {
            var startWithJ = new AdHocSpecification<string>(n => n.StartsWith("J"));

            var result = new SampleRepository().Find(!startWithJ);

            CollectionAssert.DoesNotContain(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
        }

        [Test]
        public void AndAlso_operator_is_equivalent_to_And_operator()
        {
            var spec1 = new AdHocSpecification<string>(n => n.Length > 2);
            var spec2 = new AdHocSpecification<string>(n => n.Length < 5);

            Assert.AreEqual(spec1 & spec2, spec1 && spec2);
        }

        [Test]
        public void OrElse_operator_is_equivalent_to_Or_operator()
        {
            var spec1 = new AdHocSpecification<string>(n => n.Length < 2);
            var spec2 = new AdHocSpecification<string>(n => n.Length > 5);

            Assert.AreEqual(spec1 | spec2, spec1 || spec2);
        }

        [Test]
        public void Combination_of_boolean_operators_should_work()
        {
            var startWithM = new AdHocSpecification<string>(n => n.StartsWith("M"));
            var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));
            var containsU = new AdHocSpecification<string>(n => n.Contains("u"));

            var result = new SampleRepository().Find(startWithM | (!endsWithN & !containsU));

            CollectionAssert.Contains(result, "Jose");
            CollectionAssert.DoesNotContain(result, "Julian");
            CollectionAssert.Contains(result, "Manuel");
        }
    }
}