using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
	public class NegateSpecification<T> : Specification<T>
	{
		private readonly Specification<T> spec;

		public NegateSpecification(Specification<T> spec)
		{
			this.spec = spec;
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			ParameterExpression param = Expression.Parameter(typeof(T), "x");
			return Expression.Lambda<Func<T, bool>>(
				Expression.Not(
					Expression.Invoke(spec.IsSatisfiedBy(), param)), param);
		}

		public bool Equals(NegateSpecification<T> other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Equals(other.spec, spec);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (NegateSpecification<T>)) return false;
			return Equals((NegateSpecification<T>) obj);
		}

		public override int GetHashCode()
		{
			return (spec != null ? spec.GetHashCode() : 0);
		}
	}
}