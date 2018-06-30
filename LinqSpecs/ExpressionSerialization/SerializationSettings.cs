using System;

namespace LinqSpecs
{
    /// <summary>Common serialization settings.</summary>
	public static class SerializationSettings
	{
        /// <summary>An expression serializer factory.</summary>
        public static IExpressionSerializerFactory ExpressionSerializerFactory { get; set; }
	}
}