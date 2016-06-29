using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using LinqSpecs.ExpressionSerialization;

namespace LinqSpecs
{
	[Serializable]
	public class AdHocSpecification<T> : Specification<T>
	{
	    readonly string serializedPredicate;

		public AdHocSpecification(Expression<Func<T, bool>> predicate)
		{
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            var cleanedExpression = ExpressionUtility.Ensure(predicate);
		    var serializer = new ExpressionSerializer();
		    var serializedExpression = serializer.Serialize(cleanedExpression);
		    serializedPredicate = serializedExpression.ToString();
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
		    var serializer = new ExpressionSerializer();
            var serializedExpression = XElement.Parse(serializedPredicate);
            var specification = serializer.Deserialize<Func<T, bool>>(serializedExpression);
			return specification;
		}
	}
}