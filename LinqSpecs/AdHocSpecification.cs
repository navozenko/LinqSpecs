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
        private Expression<Func<T, bool>> _predicate;

        // For serialization only
        private string _serializedPredicate;

        /// <summary>
        /// Creates a custom ad-hoc <see cref="Specification{T}"/> from the given predicate expression.
        /// </summary>
        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        /// <summary>
        /// Returns an expression that defines this query.
        /// </summary>
        public override Expression<Func<T, bool>> ToExpression()
        {
            return _predicate;
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            var cleanedExpression = ExpressionUtility.Ensure(_predicate);
            var serializer = new ExpressionSerializer();
            var xmlElement = serializer.Serialize(cleanedExpression);
            _serializedPredicate = xmlElement.ToString();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            var serializer = new ExpressionSerializer();
            var xmlElement = XElement.Parse(_serializedPredicate);
            _predicate = serializer.Deserialize<Func<T, bool>>(xmlElement);
        }
	}
}