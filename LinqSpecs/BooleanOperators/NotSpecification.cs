using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Negates a source specification.
    /// </summary>
    [Serializable]
	class NotSpecification<T> : Specification<T>
	{
		readonly Specification<T> spec;

		public NotSpecification(Specification<T> spec)
		{
            if (spec == null)
                throw new ArgumentNullException("spec");
            this.spec = spec;
		}

		public override Expression<Func<T, bool>> ToExpression()
		{
			var expr = spec.ToExpression();
			return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
		}

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (GetType() != other.GetType())
                return false;

            var otherSpec = other as NotSpecification<T>;
            return spec.Equals(otherSpec.spec);
        }

        public override int GetHashCode()
        {
            return spec.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}