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
		public override Expression<Func<T, bool>> ToExpression()
		{
			return x => true;
		}

        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return GetType() == other.GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}