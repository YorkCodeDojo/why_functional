Imports System
Imports System.Numerics

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")

        Dim empty = ListOf (Of String).Nil
        Console.WriteLine("is Nil:" + empty.IsNil().ToString())
        Dim list1 = ListOf (Of String).Cons("World", empty)
        Console.WriteLine("is Nil:" + list1.IsNil().ToString())
        Console.WriteLine(list1.Head)
        Dim list2 = ListOf (Of String).Cons("Hello", list1)
        Console.WriteLine("is Nil:" + list2.IsNil().ToString())

        Console.WriteLine(list2.ToString())

        Dim myList as ListOf(Of Integer) = ListOf (Of Integer).Cons(1, ListOf (Of Integer).Cons(2,
                                                                                                ListOf (Of Integer).Cons(
                                                                                                    3,
                                                                                                    ListOf _
                                                                                                                            ( _
                                                                                                                            Of _
                                                                                                                            Integer
                                                                                                                            ) _
                                                                                                                            .
                                                                                                                            Nil)))
        Dim total1 = sum(myList)
        Console.WriteLine("Sum of list: " + total1.ToString())

        Dim total2 = Times(myList)
        Console.WriteLine("Times of list: " + total2.ToString())

        Dim Add = Function(x, y) x + y

        Dim AddTen = foldr (Of Integer, Integer)(Add, 10, myList)
        Console.WriteLine("Foldr sum of list: " + AddTen.ToString())

        Dim values As Integer() = {1, 2, 3, 4, 5}
        Dim myList2 As ListOf(Of Integer) = BuildList(values)
        Console.WriteLine("Built List: " + sum(myList2).ToString())

        Dim bools1 = {False, False, False}
        Dim myListBools1 = BuildList(bools1)
        Console.WriteLine("Built List: " + AnyTrue(myListBools1).ToString())

        Dim bools2 = {True, True, True}
        Dim myListBools2 = BuildList(bools2)
        Console.WriteLine("Built List: " + AllTrue(myListBools2).ToString())

        Dim values232 As Integer() = {1, 2, 3, 4, 5}
        Dim myList232 As ListOf(Of Integer) = CopyList(BuildList(values232))
        Console.WriteLine("Built List: " + sum(myList232).ToString())

        Dim values2321 As Integer() = {1, 2}
        Dim values2322 As Integer() = {1, 2}
        Dim myList2323 As ListOf(Of Integer) = Append(BuildList(values2321), BuildList(values2322))
        Console.WriteLine("Built List: " + sum(myList2323).ToString())
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
        return Foldr(Function(x, y) ListOf (Of T).Cons(x, y), ListOf (Of T).Nil, list)
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
