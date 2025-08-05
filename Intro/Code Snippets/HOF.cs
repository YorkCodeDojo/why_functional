using System;
using System.Collections.Generic;

namespace Intro.Code_Snippets;

public class HOF
{

    List<int> EvenNumbers(List<int> allNumbers)
    {
        var result = new List<int>();
        foreach (var number in allNumbers)
        {
            if (number % 2 == 0)
                result.Add(number);
        }
        return result;
    }
    
    List<int> OddNumbers(List<int> allNumbers)
    {
        var result = new List<int>();
        foreach (var number in allNumbers)
        {
            if (number % 2 == 1)
                result.Add(number);
        }
        return result;
    }


    public void Example()
    {
        var even = EvenNumbers([1, 2, 3, 4, 5]);
        var odd = OddNumbers([1, 2, 3, 4, 5]);
    }
    
    // f: (int) -> bool
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
    
    public void Example2()
    {
        bool isEven(int n) => n % 2 == 0;
        bool isOdd(int n) => n % 2 == 1;
        var even = FilterNumbers(isEven, [1, 2, 3, 4, 5]);
        var odd = FilterNumbers(isOdd, [1, 2, 3, 4, 5]);
    }
    
}