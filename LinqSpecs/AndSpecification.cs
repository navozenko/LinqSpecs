using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
	public class AndSpecification<T> : Specification<T>
	{
		private readonly Specification<T> spec1;
		private readonly Specification<T> spec2;

		public AndSpecification(Specification<T> spec1, Specification<T> spec2)
		{
			this.spec1 = spec1;
			this.spec2 = spec2;
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			ParameterExpression param = Expression.Parameter(typeof(T), "x");
			return Expression.Lambda<Func<T, bool>>(
				Expression.AndAlso(
					Expression.Invoke(spec1.IsSatisfiedBy(), param),
					Expression.Invoke(spec2.IsSatisfiedBy(), param)), param);
		}

		public bool Equals(AndSpecification<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.spec1, spec1) && Equals(other.spec2, spec2);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (AndSpecification<T>)) return false;
			return Equals((AndSpecification<T>) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((spec1 != null ? spec1.GetHashCode() : 0)*397) ^ (spec2 != null ? spec2.GetHashCode() : 0);
			}
		}
	}
}