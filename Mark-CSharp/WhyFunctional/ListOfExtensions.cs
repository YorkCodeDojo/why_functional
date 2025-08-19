using System.Numerics;

namespace WhyFunctional;

public static class ListOfExtensions
{
	public static T Sum<T>(this ListOf<T> list) where T : INumber<T>
	{
		return list.IsNil
			? T.Zero
			: list.Head + Sum(list.Tail);
	}
}
