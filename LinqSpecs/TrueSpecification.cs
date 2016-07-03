using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>
    /// Specification which is satisfied by any object.
    /// </summary>
	[Serializable]
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
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
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
            return GetType().GetHashCode();
        }
    }
}