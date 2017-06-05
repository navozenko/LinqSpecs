using System;
using System.Linq.Expressions;
using LinqSpecs.ExpressionCombining;

namespace LinqSpecs
{
    /// <summary>
    /// Combines two specifications by using logical OR operation.
    /// </summary>
    [Serializable]
    internal class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _spec1;
        private readonly Specification<T> _spec2;

        public OrSpecification(Specification<T> spec1, Specification<T> spec2)
        {
            _spec1 = spec1 ?? throw new ArgumentNullException(nameof(spec1));
            _spec2 = spec2 ?? throw new ArgumentNullException(nameof(spec2));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = _spec1.ToExpression();
            var expr2 = _spec2.ToExpression();
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
            return _spec1.Equals(otherSpec._spec1) && _spec2.Equals(otherSpec._spec2);
        }

        public override int GetHashCode()
        {
            return _spec1.GetHashCode() ^ _spec2.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}