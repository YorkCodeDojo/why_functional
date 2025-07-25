
using System.Numerics;

var myListOfDoubles = Cons(1.1, Cons(2.2, Cons(3.3, Nil<double>())));
var myListOfInts = Cons(2, Cons(2, Cons(3, Nil<int>())));

Print(myListOfDoubles);
Console.WriteLine(Sum(myListOfDoubles));
Console.WriteLine(Sum2(myListOfDoubles));

Console.WriteLine(Product(myListOfInts));
Console.WriteLine(Product2(myListOfInts));

var biggerList = BuildList(1, 2, 3, 4, 5, 6, 7, 8, 9);
Print(biggerList);
Console.WriteLine(Sum2(biggerList));

Console.WriteLine(AnyTrue(BuildList(true, false))); // true
Console.WriteLine(AllTrue(BuildList(true, false))); // false

Print(Copy(biggerList));
Print(Append(biggerList, BuildList(10,20)));

//8
Console.WriteLine(Length(Append(biggerList, BuildList(10,20))));

//9
Print(DoubleAll9(Append(biggerList, BuildList(10,20))));

//10
Print(DoubleAll(Append(biggerList, BuildList(10,20))));

return;

ListOf<T> Cons<T>(T element, ListOf<T> rest)
{
    return new ListOf<T>(element, rest);
}

ListOf<T> Nil<T>()
{
    return new ListOf<T>();
}

void Print<T>(ListOf<T> list)
{
    if (list.IsNil)
    {
        Console.WriteLine();
    }
    else
    {
        Console.Write(list.Head);
        Console.Write(" ");
        Print(list.Tail);
    }
}

T Sum<T>(ListOf<T> values) where T : INumber<T>
{
    if (values.IsNil)
        return T.Zero;
    else
        return values.Head + Sum(values.Tail);
}

T Product<T>(ListOf<T> values) where T : INumber<T>
{
    if (values.IsNil)
        return T.One;
    else
        return values.Head * Product(values.Tail);
}

// Exercise 4
TResult FoldR<T, TResult>(Func<T,TResult,TResult> f, TResult initialValue, ListOf<T> values)
{
    if (values.IsNil)
        return initialValue;
    else
        return f(values.Head, FoldR(f, initialValue, values.Tail));
}

T Add<T>(T lhs, T rhs) where T : INumber<T> => lhs + rhs;
T Multiply<T>(T lhs, T rhs) where T : INumber<T> => lhs * rhs;

T Sum2<T>(ListOf<T> values) where T : INumber<T> => FoldR(Add, T.AdditiveIdentity, values);
T Product2<T>(ListOf<T> values) where T : INumber<T> => FoldR(Multiply, T.MultiplicativeIdentity, values);


// Exercise 5
ListOf<T> BuildList<T>(params T[] values)
{
    if (values.Length == 0)
        return Nil<T>();
    else
        return Cons(values[0], BuildList(values[1..]));
}


// Exercise 6
bool Or(bool lhs, bool rhs) => lhs || rhs;
bool And(bool lhs, bool rhs) => lhs && rhs;
bool AnyTrue(ListOf<bool> values) => FoldR(Or, false, values);
bool AllTrue(ListOf<bool> values) => FoldR(And, true, values);

// Exercise 7
ListOf<T> Copy<T>(ListOf<T> values) => FoldR(Cons, Nil<T>(), values);
ListOf<T> Append<T>(ListOf<T> pre, ListOf<T> post) => FoldR(Cons, post, pre);


// Exercise 8
int Count<T>(T lhs, int rhs) => rhs + 1;
int Length<T>(ListOf<T> values) => FoldR(Count, 0, values);

// Exercise 9
ListOf<T> DoubleAndCons9<T>(T n, ListOf<T> values) where T : INumber<T> => Cons((n + n), values);
ListOf<T> DoubleAll9<T>(ListOf<T> values) where T : INumber<T> => FoldR(DoubleAndCons9, Nil<T>(), values);

// ListOf<T> FAndCons<T>(Func<T,T> f, T n, ListOf<T> values) where T : INumber<T> => Cons(f(n), values);
// ListOf<T> DoubleAll10<T>(ListOf<T> values) where T : INumber<T> => FoldR((l,r) => FAndCons(Double,l,r), Nil<T>(), values);
// Print(DoubleAll10(Append(biggerList, BuildList(10,20))));

// Exercise 10
T Double<T>(T n) where T : INumber<T> => n + n;
ListOf<TResult> Map<T, TResult>(Func<T, TResult> f, ListOf<T> values) => FoldR((l,r) => Cons(f(l), r), Nil<TResult>(), values);
ListOf<T> DoubleAll<T>(ListOf<T> values) where T : INumber<T> => Map(Double, values);

class ListOf<T>
{
    private readonly T? _element;
    private readonly ListOf<T>? _rest;

    public ListOf()
    {
        IsNil = true;
    }
    
    public ListOf(T element, ListOf<T> rest)
    {
        _element = element;
        _rest = rest;
        IsNil = false;
    }

    public bool IsNil { get; }

    public T Head => _element!;
    public ListOf<T> Tail => _rest!;
}