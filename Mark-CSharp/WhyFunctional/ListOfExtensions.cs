using System.Numerics;

namespace WhyFunctional;

public static class ListOfExtensions
{
	/// <summary>
	/// Returns the summation of the values contained within <paramref name="list"/>
	/// </summary>
	/// <param name="list">A <seealso cref="ListOf{T}"/> values to be summed</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The summation result of values held in <paramref name="list"/></returns>
	public static TResult Sum<T, TResult>(this ListOf<T> list)
		where T : INumber<T>, IAdditiveIdentity<T, TResult>, IAdditionOperators<T, TResult, TResult>
		where TResult : INumber<TResult>
	{
		return list.FoldR(Add, TResult.Zero);
	}

	/// <summary>
	/// Returns the product of the values contained in <paramref name="list"/>
	/// </summary>
	/// <param name="list">A <seealso cref="ListOf{T}"/> values to be summed</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The product of values held in <paramref name="list"/></returns>
	public static TResult Product<T, TResult>(this ListOf<T> list)
		where T : INumber<T>, IMultiplicativeIdentity<T, TResult>, IMultiplyOperators<T, TResult, TResult>
		where TResult : INumber<TResult>
	{
		return list.FoldR(Mul, TResult.One);
	}

	/// <summary>
	/// An addition function for use in <see cref="FoldR{T,TResult}"/> operations
	/// </summary>
	/// <param name="a">The first value</param>
	/// <param name="b">The second value</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The summation of <paramref name="a"/> and <paramref name="b"/></returns>
	private static TResult Add<T, TResult>(T a, TResult b)
		where T : INumber<T>, IAdditiveIdentity<T, TResult>, IAdditionOperators<T, TResult, TResult>
		where TResult : INumber<TResult>
	{
		return a + b;
	}

	/// <summary>
	/// A multiplication function for use as a <see cref="FoldR{T,TResult}"/> operation
	/// </summary>
	/// <param name="a">The first value</param>
	/// <param name="b">The second value</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The product of <paramref name="a"/> and <paramref name="b"/></returns>
	private static TResult Mul<T, TResult>(T a, TResult b)
		where T : INumber<T>, IMultiplicativeIdentity<T, TResult>, IMultiplyOperators<T, TResult, TResult>
		where TResult : INumber<TResult>
	{
		return a * b;
	}

	/// <summary>
	/// Provides a Fold-Right function to perform the required operation (<paramref name="fn"/>) on the <paramref name="list"/>, starting with the initial value <paramref name="initial"/>
	/// </summary>
	/// <param name="list">The <see cref="ListOf{T}"/> to be manipulated</param>
	/// <param name="fn">The function manipulating the <paramref name="list"/></param>
	/// <param name="initial">The initial value for the function</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The result of applying <paramref name="fn"/> over the <paramref name="list"/></returns>
	private static TResult FoldR<T, TResult>(this ListOf<T> list, Func<T, TResult, TResult> fn, TResult initial)
		where T : notnull
		where TResult : notnull
	{
		return list.IsNil
			? initial
			: fn(list.Head, list.Tail.FoldR(fn, initial));
	}

	/// <summary>
	/// Helper method to create a <see cref="ListOf{T}"/> from an array of values
	/// </summary>
	/// <param name="values">The values to be added to the output</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>A <see cref="List{T}"/> object, containing all values in <paramref name="values"/></returns>
	public static ListOf<T> BuildList<T>(this T[] values)
		where T : notnull
	{
		return values.Length > 0
			? ListOf<T>.Cons(values[0], BuildList(values[1..]))
			: ListOf<T>.Nil();
	}

	/// <summary>
	/// Provides a logical OR operation for two values
	/// </summary>
	/// <param name="a">The first value</param>
	/// <param name="b">The second value</param>
	/// <returns>The logical OR product</returns>
	private static bool LogicalOr(bool a, bool b) => a || b;

	/// <summary>
	/// Provides a logical AND operation for two values
	/// </summary>
	/// <param name="a">The first value</param>
	/// <param name="b">The second value</param>
	/// <returns>The logical AND product</returns>
	private static bool LogicalAnd(bool a, bool b) => a && b;

	/// <summary>
	/// Determines whether any of the <paramref name="values"/> are true
	/// </summary>
	/// <param name="values">A <see cref="ListOf{T}"/> object with boolean values</param>
	/// <returns>True if any values are true, otherwise false</returns>
	public static bool AnyTrue(ListOf<bool> values) => values.FoldR(LogicalOr, false);

	/// <summary>
	/// Determines whether ALL of the <paramref name="values"/> are true
	/// </summary>
	/// <param name="values">A <see cref="ListOf{T}"/> object with boolean values</param>
	/// <returns>True if ALL values are true, otherwise false</returns>
	public static bool AllTrue(ListOf<bool> values) => values.FoldR(LogicalAnd, true);

	/// <summary>
	/// Copies the contents of <paramref name="list"/> into a new <see cref="ListOf{T}"/>
	/// </summary>
	/// <param name="list">The list to be copied</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>A copy of <paramref name="list"/></returns>
	public static ListOf<T> Copy<T>(this ListOf<T> list)
		where T : notnull
	{
		return list.FoldR(ListOf<T>.Cons, ListOf<T>.Nil());
	}

	/// <summary>
	/// Appends the contents of <paramref name="second"/> to <paramref name="first"/>, returnin a new <see cref="ListOf{T}"/>
	/// </summary>
	/// <param name="first">The first part of the <see cref="ListOf{T}"/></param>
	/// <param name="second">The second part of the <see cref="ListOf{T}"/></param>
	/// <typeparam name="T"></typeparam>
	/// <returns>A new <see cref="ListOf{T}"/> object containing elements of both <paramref name="first"/> and <paramref name="second"/></returns>
	public static ListOf<T> Append<T>(this ListOf<T> first, ListOf<T> second)
		where T : notnull
	{
		return first.FoldR(ListOf<T>.Cons, second);
	}
}