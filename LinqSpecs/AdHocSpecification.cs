using System;
using System.Linq.Expressions;
using System.Xml.Linq;
using LinqSpecs.ExpressionSerialization;

namespace LinqSpecs
{
	[Serializable]
	public class AdHocSpecification<T> : Specification<T>
	{
		//private readonly Expression<Func<T, bool>> specification;
	    private readonly String serializedExpressionXml;

		public AdHocSpecification(Expression<Func<T, bool>> specification)
		{
			//this.specification = specification;
		    var serializer = new ExpressionSerializer();
		    var serializedExpression = serializer.Serialize(specification);
		    serializedExpressionXml = serializedExpression.ToString();
		}

		public override Expression<Func<T, bool>> IsSatisfiedBy()
		{
		    var serializer = new ExpressionSerializer();
            var serializedExpression = XElement.Parse(serializedExpressionXml);
            var specification = serializer.Deserialize<Func<T, bool>>(serializedExpression);
			return specification;
		}
	}
}