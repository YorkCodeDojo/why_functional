using System.Numerics;

namespace WhyFunctional;

public class ListOf<T>
	where T : INumber<T>
{
	#region Fields

	/// <summary>
	/// Holds the first value for the object
	/// </summary>
	private readonly T _value = default(T)!;

	/// <summary>
	/// Holds all other values for the object
	/// </summary>
	private readonly ListOf<T> _others = null!;

	#endregion

	#region Properties

	/// <summary>
	/// Helper property to indicate whether the value of the object is Nil
	/// </summary>
	public bool IsNil { get; }

	/// <summary>
	/// Helper property converts all values into an array
	/// </summary>
	public T[] ToArray
	{
		get
		{
			return IsNil
				? Array.Empty<T>()
				: new T[] { _value }.Concat(_others.ToArray).ToArray();
		}
	}

	/// <summary>
	/// Returns the value at the "head" of the <see cref="ListOf{T}"/>
	/// </summary>
	public T Head => _value;

	/// <summary>
	/// Returns the value of the other items in the <see cref="ListOf{T}"/>
	/// </summary>
	public ListOf<T> Tail => _others;

	#endregion

	#region Constructors

	/// <summary>
	/// Default ctor - creates an object equivalent to <see cref="Nil"/>
	/// </summary>
	private ListOf()
	{
		IsNil = true;
	}

	/// <summary>
	/// Alternate ctor - must supply both values as non-null
	/// </summary>
	/// <param name="value">The first value</param>
	/// <param name="others">The other values</param>
	private ListOf(T value, ListOf<T> others)
	{
		ArgumentNullException.ThrowIfNull(value, nameof(value));
		ArgumentNullException.ThrowIfNull(others, nameof(others));
		_value = value;
		_others = others;
		IsNil = false;
	}

	#endregion

	#region Methods

	/// <summary>
	/// Static method returns the equivalent of an object with an empty array
	/// </summary>
	/// <returns>The newly created empty object</returns>
	public static ListOf<T> Nil()
	{
		return new ListOf<T>();
	}

	/// <summary>
	/// Static "constructor" to initialise the object with two non-null values
	/// </summary>
	/// <param name="value">The first value for the object</param>
	/// <param name="others">All other values</param>
	/// <returns>The newly created object containing the specified values</returns>
	public static ListOf<T> Cons(T value, ListOf<T> others)
	{
		return new ListOf<T>(value, others);
	}

	#endregion

	public override string ToString()
	{
		return IsNil
			? "Nil"
			: $"[{Head}, {Tail}]";
	}
}