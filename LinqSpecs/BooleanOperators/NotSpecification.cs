using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Negates a source specification.
    /// </summary>
    internal class NotSpecification<T> : Specification<T>
    {
        public Specification<T> Source { get; }

        public NotSpecification(Specification<T> source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr = Source.ToExpression();
            return Expression.Lambda<Func<T, bool>>(Expression.Not(expr.Body), expr.Parameters);
        }

        public override bool Equals(object other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other is NotSpecification<T> otherSpec)
                return Source.Equals(otherSpec.Source);
            return false;
        }

        public override int GetHashCode()
        {
            return Source.GetHashCode() ^ GetType().GetHashCode();
        }
    }
}
