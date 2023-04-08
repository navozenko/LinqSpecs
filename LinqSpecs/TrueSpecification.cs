using System;
using System.Linq.Expressions;
using LinqSpecs.Utilities;

namespace LinqSpecs
{
    /// <summary>
    /// Specification which is satisfied by any object.
    /// </summary>
    public class TrueSpecification<T> : Specification<T>
    {
        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return x => true;
        }
        
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        public override bool Equals(object? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return GetType() == other.GetType();
        }
        
        /// <summary>
        /// Returns a hash code for the current object.
        /// </summary>
        public override int GetHashCode()
        {
            return HashCodeHelpers.Combine(GetType());
        }
    }
}
