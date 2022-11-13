using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Allows to write a <see cref="Specification{T}"/> without writing a class.
    /// </summary>
    public class AdHocSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> _predicate;

        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification{T}"/> from the given predicate expression.
        /// </summary>
        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate;
        }
    }
}
