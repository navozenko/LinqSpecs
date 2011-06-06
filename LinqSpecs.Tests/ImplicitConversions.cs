using System;
using System.Linq.Expressions;
using NUnit.Framework;
using SharpTestsEx;

namespace LinqSpecs.Tests
{
    [TestFixture]
    public class ImplicitConversions
    {
        [Test]
        public void can_implicitly_convert_spec_to_expr()
        {
            Expression<Func<string, bool>> expr = new AdHocSpecification<string>(s => s.Length == 2);
            expr.Compile().Invoke("ar").Should().Be.True();
        }

        [Test]
        public void can_implicitly_convert_expr_to_spec()
        {
            Expression<Func<string, bool>> expr = s => s.Length == 2;
            Specification<string> spec = expr;
            spec.IsSatisfiedBy().Compile().Invoke("ar").Should().Be.True();
        }

        [Test]
        public void sample_0()
        {
            Expression<Func<string, bool>> expr1 = s => s.Length == 2;
            Expression<Func<string, bool>> expr2 = s => s.StartsWith("a");
            Expression<Func<string, bool>> expr = (Specification<string>)expr1 & expr2;

            expr.Compile()("ar").Should().Be.True();
        }
        [Test]
        public void sample_1()
        {
            Expression<Func<string, bool>> expr = s => s.Length == 2;
            Specification<string> spec = expr;
            Action<Expression<Func<string, bool>>> funcWithExprParam = e => e.Should().Be.SameInstanceAs(expr); //do nothing
            funcWithExprParam(spec);
        }


        [Test]
        public void sample_2()
        {
            var spec1 = new AdHocSpecification<string>(s => s.Length == 2);
            var spec2 = new AdHocSpecification<string>(s => s.StartsWith("a"));
            Expression<Func<string, bool>> expr = spec1 & spec2;

            expr.Compile()("ar").Should().Be.True();
        }

        [Test]
        public void sample_3()
        {
            var spec1 = new AdHocSpecification<string>(s => s.Length == 2);
            Expression<Func<string, bool>> expr = spec1 & ((Expression<Func<string, bool>>)(s => s.StartsWith("a")));
            expr.Compile()("ar").Should().Be.True();
        }
    }
}