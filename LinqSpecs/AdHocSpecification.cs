using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Xml.Linq;
using LinqSpecs.ExpressionSerialization;

namespace LinqSpecs
{
    /// <summary>
    /// Allows to write a <see cref="Specification{T}"/> without writing a class.
    /// </summary>
    [Serializable]
	public class AdHocSpecification<T> : Specification<T>
	{
        [NonSerialized]
        Expression<Func<T, bool>> predicate;

        // For serialization only
        string serializedPredicate;

        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification{T}"/> from the given predicate expression.
        /// </summary>
        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException("predicate");

            this.predicate = predicate;
        }

        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return predicate;
        }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            var cleanedExpression = ExpressionUtility.Ensure(predicate);
            var serializer = new ExpressionSerializer();
            var xmlElement = serializer.Serialize(cleanedExpression);
            serializedPredicate = xmlElement.ToString();
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext context)
        {
            var serializer = new ExpressionSerializer();
            var xmlElement = XElement.Parse(serializedPredicate);
            predicate = serializer.Deserialize<Func<T, bool>>(xmlElement);
        }
	}
}