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
		where T : notnull, INumber<T>, IAdditiveIdentity<T, TResult>, IAdditionOperators<T, TResult, TResult>
		where TResult : notnull, INumber<TResult>
	{
		return list.FoldR<T, TResult>(Add<T, TResult>, TResult.Zero);
	}

	/// <summary>
	/// Returns the product of the values contained in <paramref name="list"/>
	/// </summary>
	/// <param name="list">A <seealso cref="ListOf{T}"/> values to be summed</param>
	/// <typeparam name="T"></typeparam>
	/// <typeparam name="TResult"></typeparam>
	/// <returns>The product of values held in <paramref name="list"/></returns>
	public static TResult Product<T, TResult>(this ListOf<T> list)
		where T : notnull, INumber<T>, IMultiplicativeIdentity<T, TResult>, IMultiplyOperators<T, TResult, TResult>
		where TResult : notnull, INumber<TResult>
	{
		return list.FoldR(Mul<T, TResult>, TResult.One);
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
		where T : notnull, INumber<T>, IAdditiveIdentity<T, TResult>, IAdditionOperators<T, TResult, TResult>
		where TResult : notnull, INumber<TResult>
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
		where T : notnull, INumber<T>, IMultiplicativeIdentity<T, TResult>, IMultiplyOperators<T, TResult, TResult>
		where TResult : notnull, INumber<TResult>
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
	public static TResult FoldR<T, TResult>(this ListOf<T> list, Func<T, TResult, TResult> fn, TResult initial)
		where T : notnull, INumber<T>
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
		where T : notnull, INumber<T>
	{
		return values.Length > 0
			? ListOf<T>.Cons(values[0], BuildList<T>(values[1..]))
			: ListOf<T>.Nil();
	}
}