using System;
using System.Collections.Generic;

namespace Intro.Code_Snippets;

public class PF
{
    // f: (int) -> bool
    // FilterNumbers::  ((int) -> bool,  List<int>) -> List<int>
    List<int> FilterNumbers(Func<int,bool> f, List<int> allNumbers)
    {
        var result = new List<int>();
        foreach (var number in allNumbers)
        {
            if (f(number))
                result.Add(number);
        }
        return result;
    }
    
    public void Example()
    {
        // isEven:: (int) -> bool
        bool isEven(int n) => n % 2 == 0;
        
        // FilterNumbers::  ((int) -> bool,  List<int>) -> List<int>
        var even = FilterNumbers(isEven, [1, 2, 3, 4, 5]);

        // evenNumbers::  (List<int>) -> List<int>
        var evenNumbers = Partial(FilterNumbers, isEven);

        var evens = evenNumbers([1, 2, 3, 4, 5]);

    }

    private static Func<List<int>,List<int>> Partial(Func<Func<int, bool>, List<int>,List<int>> f, Func<int, bool> arg1)
    {
        return (list) => f(arg1, list);
    }
}