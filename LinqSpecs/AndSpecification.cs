// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndSpecification.cs" company="">
//   
// </copyright>
// <summary>
//   The and specification.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LinqSpecs
{
	using System;
	using System.Linq.Expressions;

	/// <summary>
	/// The and specification.
	/// </summary>
	/// <typeparam name="T">
	/// </typeparam>
	public class AndSpecification<T> : Specification<T>
	{
		private readonly Specification<T> spec1;

		private readonly Specification<T> spec2;

		public AndSpecification(Specification<T> spec1, Specification<T> spec2)
		{
			this.spec1 = spec1;
			this.spec2 = spec2;
		}

		public bool Equals(AndSpecification<T> other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return Equals(other.spec1, this.spec1) && Equals(other.spec2, this.spec2);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != typeof(AndSpecification<T>))
			{
				return false;
			}

			return this.Equals((AndSpecification<T>)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((this.spec1 != null ? this.spec1.GetHashCode() : 0) * 397) ^
					   (this.spec2 != null ? this.spec2.GetHashCode() : 0);
			}
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
			return this.spec1.IsSatisfiedBy().AndAlso(this.spec2.IsSatisfiedBy());
		}
	}
}