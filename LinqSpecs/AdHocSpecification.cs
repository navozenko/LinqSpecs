using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Allows to write a <see cref="Specification{T}"/> without writing a class.
    /// </summary>
    public class AdHocSpecification<T> : Specification<T>
    {
        public Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification{T}"/> from the given predicate expression.
        /// </summary>
        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Predicate;
        }
    }
}
