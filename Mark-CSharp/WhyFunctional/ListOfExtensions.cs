using System.Numerics;

namespace WhyFunctional;

public static class ListOfExtensions
{
	/// <summary>
	/// Returns the summation of the values contained within <paramref name="list"/>
	/// </summary>
	/// <param name="list">A <seealso cref="ListOf{T}"/> values to be summed</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>The summation result of values held in <paramref name="list"/></returns>
	public static T Sum<T>(this ListOf<T> list) where T : INumber<T>
	{
		return list.IsNil
			? T.Zero
			: list.Head + Sum(list.Tail);
	}

	/// <summary>
	/// Returns the product of the values contained in <paramref name="list"/>
	/// </summary>
	/// <param name="list">A <seealso cref="ListOf{T}"/> values to be summed</param>
	/// <typeparam name="T"></typeparam>
	/// <returns>The product of values held in <paramref name="list"/></returns>
	public static T Product<T>(this ListOf<T> list) where T : INumber<T>
	{
		return list.IsNil
			? T.One
			: list.Head * Product(list.Tail);
	}
}