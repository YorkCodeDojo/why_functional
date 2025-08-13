# Why Functional Programming Matters
By John Hughes


https://www.cs.kent.ac.uk/people/staff/dat/miranda/whyfp90.pdf


## Tonight

We will be implementing the code from section 3 - 'Gluing Functions Together'

You can either read the section of the paper yourself,  or follow my exercises below. (Or a combination)

No Loops allowed!





## Exercise 1 - Modelling a ListOf *

A `listOf` can be empty,  this is called Nil.

or

A `liistOf *` can contain an Element and another `listOf *` 


The list should only contain elements of a single type.  ie.  A listOf shouldn't contain both strings and integers.



### Tasks

1. Decide how you want to model your listof data structure.  This will vary depending on the language you are using.  For example in C# I used a Class but in TypeScript (and F#) I defined a type.


2. Write a function Nil() which returns an empty list.

```
myEmptyList = nil()
```


3. A `listOf` is constructed using the `Cons` function.  This takes two arguments,  an element and another listOf object.


Some examples

|      |                             |       |
|----------------|--------------------------------|---------|
| Empty List     | Nil                            | []      |
| Single Element | Cons(1, Nil)                   | [1]     |
| Three Elements | Cons(1, Cons(2, Cons(3, Nil))) | [1,2,3] |

Write a TWO argument function `Cons(x, y)` which returns a populated list.


Hints:

For those using classes....
Ideally, the `listOf` object should be a generic container.  Ie. a `ListOf<T>`.  However it is possible to complete most of these exercises if your object can only contain integers.


## Exercise 2 - Querying the ListOf object

With this data structure there should be no way to directly access the nth element of the list.

### Class

If you have defined your ListOf datastructure using a class then....


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

### Types

If you have defined your ListOf datastructure using a types then I expect these methods aren't required.





## Exercise 3 - Summing the list

We can now write a method to sum all the values in our list.  Remember, loops aren't allowed.

```
var myList = Cons(1, Cons(2, Cons(3, Nil())))
var total = sum(myList) // 1+2+3 = 6

```

Hints:

Start with the case where the list is empty.



## Exercise 4 - Multipling the list

Using your knowledge gained from writting the `sum` method,  write a new method which multiplies all the values.

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
print( foldr( (*), 1, myList )  ) // Multiplies all the values in my List
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


## Exercise 5 - Simple List Construction

As building a list with nested `Cons` is a bit of a pain.  Create a helper function which builds the list from a simple array.

For example

```
    // var myList = Cons(13, Cons(27, Cons(3, Nil())))
    var myList = BuildList([13, 27, 3])
```

You should be able to do this without changing your existing `ListOf` object (or using any loops!)


## Exercise 6 - Bool Logic

Using your `foldr` create the following two functions:

1. `AnyTrue` - returns `true` if at least one value in a list of booleans is true.
2. `AllTrue` - returns `true` if all values in a list of booleans are true.


For example
```
AnyTrue( BuildList(true, false) ) // true
AllTrue( BuildList(true, false) ) // false
```


## Exercise 7 - Copy and Append

One way to understand `(foldr f a)` is as a function that replaces all occurrences of `Cons` in a list by `f`, and all occurrences of `Nil` by `a`. 

Taking the list `[1,2,3]` as an example, since this means `Cons 1 (Cons 2 (Cons 3 Nil ))` 
then `(foldr (+) 0)` converts it into `(+) 1 ((+) 2 ((+) 3 0)) = 6` 

Still using `foldr` can you now write a function which copies a list?

And then can you modify your copy function to create one which appends 2 lists together.

```
append([1,2], [3.4]) // [1,2,3,4]
```


## Exercise 8 - Length

Using `foldr` create a function called `length` which counts the number of entries in a list.


```
length([10,50]) // 2
```

## Exercise 9 - Double-All

Using `foldr` create a function called `DoubleAll` which doubles the values of the entries in a list.

For example

```
DoubleAll([1,2,3]) // [2,4,6]
```

Hints

* First create a function called `Double(n: number) => number`
* As you cannot modify a list you will need to 'construct' a new one where each value is doubled.



## Exercise 10 - Map

Now copy your `DoubleAll` function and rename it to `Map`.  Replace the call to `Double` with a function which Map will need to accept as it's first argument.

You should now be able to redefine `DoubleAll` as

```
DoubleAll(list) => Map(Double, list)
```



## Exercise 11 - Tree
If you get this far! - Then refer to the paper to see how you can construct and query a tree using this ListOf datastructure.