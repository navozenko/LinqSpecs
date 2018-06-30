using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

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
        private byte[] _serializedPredicate;

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
            _serializedPredicate = CreateSerializer().Serialize(_predicate);
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            _predicate = (Expression<Func<T, bool>>)CreateSerializer().Deserialize(_serializedPredicate);
        }

        private static IExpressionSerializer CreateSerializer()
        {
            var serializerFactory = SerializationSettings.ExpressionSerializerFactory;

            if (serializerFactory == null)
            {
                throw new InvalidOperationException(
                    $"It is necessary to specify a serializer factory in the property " +
                    $"{nameof(SerializationSettings)}.{nameof(SerializationSettings.ExpressionSerializerFactory)} " +
                    $"for AdHocSpecification serialization.");
            }

            return serializerFactory.CreateSerializer();
        }
	}
}