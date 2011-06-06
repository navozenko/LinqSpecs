using System;
using System.Linq.Expressions;
using System.Linq;

namespace LinqSpecs
{
	public abstract class Specification<T>
	{
		public abstract Expression<Func<T, bool>> IsSatisfiedBy();

	    public static bool operator false(Specification<T> spec1)
	    {
	        return false; // no-op. & and && do exactly the same thing.
	    }

	    public static bool operator true(Specification<T> spec1)
	    {
	        return false; // no - op. & and && do exactly the same thing.
	    }

	    public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
		{
			return new AndSpecification<T>(spec1, spec2);
		}

	    public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
		{
			return new OrSpecification<T>(spec1, spec2);
		}

		public static Specification<T> operator !(Specification<T> spec1)
		{
			return new NegateSpecification<T>(spec1);
		}

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec.IsSatisfiedBy();
        }

        public static implicit operator Specification<T>(Expression<Func<T, bool>> expr)
        {
            return new AdHocSpecification<T>(expr);
        }

		protected virtual object[] Parameters { get { return new object[] {Guid.NewGuid()}; } }

		public bool Equals(Specification<T> other)
		{
			return Parameters.SequenceEqual(other.Parameters);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != GetType()) return false;
			return Equals((Specification<T>)obj);
		}

		public override int GetHashCode()
		{
			return Parameters.GetHashCode();
		}

	}
}