namespace LinqSpecs
{
	using System;
	using System.Linq.Expressions;

	/// <summary>
	/// The expression extensions.
	/// </summary>
	public static class ExpressionExtensions
	{
		/// <summary>
		/// The and also.
		/// </summary>
		/// <param name="expr1">
		/// The expr 1.
		/// </param>
		/// <param name="expr2">
		/// The expr 2.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// </returns>
		public static Expression<Func<T, bool>> AndAlso<T>(this Expression<Func<T, bool>> expr1,
														   Expression<Func<T, bool>> expr2)
		{
			// need to detect whether they use the same
			// parameter instance; if not, they need fixing
			ParameterExpression param = expr1.Parameters[0];
			if (ReferenceEquals(param, expr2.Parameters[0]))
			{
				// simple version
				return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), param);
			}

			// otherwise, keep expr1 "as is" and invoke expr2
			return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, param)),
					param);
		}
	}
}