using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
	[Serializable]
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
				Expression.Not(spec.IsSatisfiedBy().Body), spec.IsSatisfiedBy().Parameters);
		}

		protected override object[] Parameters
		{
			get
			{
				return new[] {spec};
			}
		}
	}
}