## Rules
No Loops allowed


## Exercise 1 - Creating the ListOf object

A `listOf` can be empty,  this is called Nil.

A `listOf` can also be constructed using the `Cons` function.  This takes two arguments,  an element and another listOf object.


### Tasks
1. Write a function Nil() which returns an empty list.
2. Write a function Cons(x, y) which returns a populated list.

For example,

This builds the list containing 1,2 and 3

```
var myList = Cons(1, Cons(2, Cons(3, Nil())))
```

Hints:

A 'Nil' list can be represented in multiple ways.  The simpliest (but maybe not the cleanest) is to add a `IsNil` property on the `listOf` object.  

Ideally, the `listOf` object should be a generic container.  Ie. a `ListOf<T>`.  However it is possible to complete all these exercises if your object can only contain integers.


## Exercise 2 - Querying the ListOf object

Now we have our list we need to able to query values from it.  There are __only__ three available methods.

1.  IsNil() --> returns true if the list is empty
2.  Head() --> returns the first element in the list
3.  Tail() --> returns the remainder of the list as a ListOf object.

For example,

```
var myList = Cons(13, Cons(27, Cons(3, Nil())))
print(myList.IsNil)  // False
print(myList.Head()) // 13
print(myList.Tail().Head()) // 27
```


## Exercise 3 - Summing the list

We can now write a method to sum all the values in our list.  Remember, loops aren't allowed.

```
var myList = Cons(1, Cons(2, Cons(3, Nil())))
var total = sum(myList) // 1+2+3 = 6

```

Hints:

Start with the case where the list is empty.



## Exercise 4 - Multipling the list

Using your knowledge gained from writting the `sum` method,  write a new method which multiples all the values.

```
var myList = Cons(2, Cons(2, Cons(3, Nil())))
var total = product(myList) // 2*2*3 = 12

```

Hints:

Watch out - your base case is slightly different,  but you can assume the list passed originally to the `product` function will contain at least one value.


## Exercise 5 - Foldr - Higher-order-function

Hopefully your implementations of `Sum` and `Product` have a similar shape.

These can be now be combined by writing a new function called FoldR (fold right)

This has the following structure

```
foldr( operation,  initialValue,  list )
```

For example

```
print( foldr( (+), 0, myList )  ) // Sums all the values in my List
print( foldr( (*), 1, myList )  ) // Multiples all the values in my List
```

The first argument to foldR expects a 'binary' function.  A binary function performs an operation on two arguments to produce a third.

For example

```
Add (x, y) => x + y

foldr( Add, 0, myList )

```

Hints

Copy your existing sum function and called it foldr

Add a new argument for the initial value,  replace the 0 with this

Add a new argument for the binary function.  Replace the + sign with a call to this function.


---

Now you can re-write your sum and product functions using foldr.

For example

```
sum(myList) => return foldr( Add, 0, myList )
```


