using System;
using System.Linq.Expressions;
using LinqSpecs.ExpressionCombining;

namespace LinqSpecs
{
    /// <summary>
    /// Combines two specifications by using logical OR operation.
    /// </summary>
    [Serializable]
    class OrSpecification<T> : Specification<T>
    {
        readonly Specification<T> spec1;
        readonly Specification<T> spec2;

        public OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            if (spec1 == null)
                throw new ArgumentNullException("spec1");
            if (spec2 == null)
                throw new ArgumentNullException("spec2");

            this.spec1 = spec1;
            this.spec2 = spec2;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = spec1.ToExpression();
            var expr2 = spec2.ToExpression();
            return expr1.OrElse(expr2);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;

            var otherSpec = other as OrSpecification<T>;
            return spec1.Equals(otherSpec.spec1) && spec2.Equals(otherSpec.spec2);
        }

        public override int GetHashCode()
        {
            return spec1.GetHashCode() ^ spec2.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}