using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
	public class AdHocSpecification<T> : Specification<T>
	{
		private readonly Expression<Func<T, bool>> specification;

		public AdHocSpecification(Expression<Func<T, bool>> specification)
		{
			this.specification = specification;
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			return specification;
		}
	}
}