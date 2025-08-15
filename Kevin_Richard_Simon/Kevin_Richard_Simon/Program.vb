Imports System
Imports System.Numerics

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")

        Dim emptyStringList = ListOf (Of String).Nil
        Console.WriteLine("is Nil:" + emptyStringList.IsNil().ToString())
        Dim worldList = ListOf (Of String).Cons("World", emptyStringList)
        Console.WriteLine("is Nil:" + worldList.IsNil().ToString())
        Console.WriteLine(worldList.Head)
        Dim helloWorldList = ListOf (Of String).Cons("Hello", worldList)
        Console.WriteLine("is Nil:" + helloWorldList.IsNil().ToString())

        Console.WriteLine(helloWorldList.ToString())

        Dim integerList as ListOf(Of Integer) = ListOf (Of Integer).Cons(1, ListOf (Of Integer).Cons(2,
                                                                                                     ListOf (Of Integer) _
                                                                                                        .Cons(
                                                                                                            3,
                                                                                                            ListOf _
                                                                                                                 ( _
                                                                                                                 Of _
                                                                                                                 Integer
                                                                                                                 ) _
                                                                                                                 .Nil)))
        Dim sumOfIntegerList = sum(integerList)
        Console.WriteLine("Sum of list: " + sumOfIntegerList.ToString())

        Dim productOfIntegerList = Times(integerList)
        Console.WriteLine("Times of list: " + productOfIntegerList.ToString())

        Dim addFunction = Function(x, y) x + y

        Dim foldrSumWithTen = foldr (Of Integer, Integer)(addFunction, 10, integerList)
        Console.WriteLine("Foldr sum of list: " + foldrSumWithTen.ToString())

        Dim numbersToBuild As Integer() = {1, 2, 3, 4, 5}
        Dim builtIntegerList As ListOf(Of Integer) = BuildList(numbersToBuild)
        Console.WriteLine("Built List: " + sum(builtIntegerList).ToString())

        Dim boolArrayAllFalse = {False, False, False}
        Dim allFalseList = BuildList(boolArrayAllFalse)
        Console.WriteLine("Built List: " + AnyTrue(allFalseList).ToString())

        Dim boolArrayAllTrue = {True, True, True}
        Dim allTrueList = BuildList(boolArrayAllTrue)
        Console.WriteLine("Built List: " + AllTrue(allTrueList).ToString())

        Dim numbersToCopy As Integer() = {1, 2, 3, 4, 5}
        Dim copiedIntegerList As ListOf(Of Integer) = CopyList(BuildList(numbersToCopy))
        Console.WriteLine("Built List: " + sum(copiedIntegerList).ToString())

        Dim firstAppendArray As Integer() = {1, 2}
        Dim secondAppendArray As Integer() = {1, 2}
        Dim appendedIntegerList As ListOf(Of Integer) = Append(BuildList(firstAppendArray), BuildList(secondAppendArray))
        Console.WriteLine("Built List: " + sum(appendedIntegerList).ToString())
    End Sub

    Function AnyTrue(list As ListOf(Of Boolean)) As Boolean
        return Foldr(Function(x, y) x Or y, False, list)
    End Function

    Function AllTrue(list As ListOf(Of Boolean)) As Boolean
        return Foldr(Function(x, y) x And y, True, list)
    End Function

    Function BuildList (Of T)(values as T()) as ListOf(Of T)
        if (values.Length = 0)
            return ListOf (Of T).Nil
        End If
        return ListOf (Of T).Cons(values(0), BuildList(values.Skip(1).ToArray()))
    End Function

    Function CopyList (Of T)(list As ListOf(Of T)) As ListOf(Of T)
        return Foldr(AddressOf ListOf (Of T).Cons, ListOf (Of T).Nil, list)
    End Function

    Function Append (Of T)(list1 As ListOf(Of T), list2 As ListOf(Of T)) As ListOf(Of T)
        return Foldr(Function(x, y) ListOf (Of T).Cons(x, y), list2, list1)
    End Function

    Function Foldr (Of T, TReturn)(func As Func(Of T, TReturn, TReturn), initial As TReturn, list As ListOf(Of T)) _
        As TReturn
        If list.IsNil() Then
            Return initial
        Else
            Return func(list.Head, Foldr(func, initial, list.Tail))
        End If
    End Function

    Function Sum(list As ListOf(Of Integer)) As Integer
        If list.IsNil() Then
            Return 0
        Else
            Return list.Head + Sum(list.Tail)
        End If
    End Function

    Function Times(list As ListOf(Of Integer)) As Integer
        If list.IsNil() Then
            Return 1
        Else
            Return list.Head*Times(list.Tail)
        End If
    End Function
End Module

Class ListOf (Of T)
    Public Property Head As T
    Public Property Tail As ListOf(Of T)

    Private Sub New()
    End Sub

    Public Overrides Function ToString() As String
        Return Head.ToString() + If(Tail.IsNil(), "", Tail.ToString())
    End Function

    Public Shared ReadOnly Nil As ListOf(Of T) = New ListOf(Of T)()

    Public Shared Function Cons(arg1 as T, arg2 as ListOf(Of T)) as ListOf(Of T)
        Dim result As New ListOf(Of T)()
        result.Head = arg1
        result.Tail = arg2
        Return result
    End Function

    Public Function IsNil() as Boolean
        return Me Is Nil
    End Function
End Class
