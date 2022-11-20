using System;
using System.Linq.Expressions;
using LinqSpecs.Utilities;

namespace LinqSpecs
{
    /// <summary>
    /// Allows to write a <see cref="Specification{T}"/> without writing a class.
    /// </summary>
    public class AdHocSpecification<T> : Specification<T>
    {
        private readonly Lazy<string> _predicateString;

        public Expression<Func<T, bool>> Predicate { get; }

        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification{T}"/> from the given predicate expression.
        /// </summary>
        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _predicateString = new Lazy<string>(() => Predicate.ToString());
        }

        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return Predicate;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other is AdHocSpecification<T> otherSpec)
                return _predicateString.Value.Equals(otherSpec._predicateString.Value);
            return false;
        }

        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCodeHelpers.Combine(_predicateString.Value, GetType());
        }
    }
}
