using System;
using System.Linq.Expressions;
using LinqSpecs.Utilities;

namespace LinqSpecs.Operators
{
    /// <summary>
    /// Combines two specifications by using logical OR operation.
    /// </summary>
    public class OrSpecification<T> : Specification<T>
    {
        public Specification<T> Left { get; }
        public Specification<T> Right { get; }

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var expr1 = Left.ToExpression();
            var expr2 = Right.ToExpression();
            return expr1.OrElse(expr2);
        }

        public override bool Equals(object? other)
        {
            if (other is null)
                return false;
            if (ReferenceEquals(this, other))
                return true;
            if (other is OrSpecification<T> otherSpec)
                return Left.Equals(otherSpec.Left) && Right.Equals(otherSpec.Right);
            return false;
        }

        public override int GetHashCode()
        {
            return HashCodeHelpers.Combine(Left, Right, GetType());
        }
    }
}
