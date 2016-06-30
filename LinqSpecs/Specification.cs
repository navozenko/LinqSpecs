using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqSpecs
{
	[Serializable]
	public abstract class Specification<T>
	{
		public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> spec)
        {
            return spec.ToExpression();
        }

		public static bool operator false(Specification<T> spec)
		{
			return false; // no-op. & and && do exactly the same thing.
		}

		public static bool operator true(Specification<T> spec)
		{
			return false; // no-op. & and && do exactly the same thing.
		}

        public static Specification<T> operator &(Specification<T> spec1, Specification<T> spec2)
        {
            return new AndSpecification<T>(spec1, spec2);
        }

        public static Specification<T> operator |(Specification<T> spec1, Specification<T> spec2)
		{
			return new OrSpecification<T>(spec1, spec2);
		}

		public static Specification<T> operator !(Specification<T> spec)
		{
			return new NegateSpecification<T>(spec);
		}
	}
}