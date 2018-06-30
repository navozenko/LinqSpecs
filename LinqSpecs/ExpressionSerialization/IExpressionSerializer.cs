using System;
using System.Linq.Expressions;

namespace LinqSpecs
{
    /// <summary>Serializes and deserializes an instance of an expression.</summary>
    public interface IExpressionSerializer
    {
        /// <summary>Serializes a specified expression to a byte array.</summary>
        byte[] Serialize(Expression expression);

        /// <summary>Deserializes an expression from a specified byte array.</summary>
        Expression Deserialize(byte[] data);
    }
}