type Nil = 'NILL'

type ListOf<T> = Nil | {element: T, rest: ListOf<T> }

const cons = <T>(element: T, rest: ListOf<T> ): ListOf<T>  => {
    return { element, rest}
}

const nil = <T>(): ListOf<T> => 'NILL'

const myList = cons(1, cons(2, cons(3, cons(4, nil()))));

// Exercise 3
const sumList = <T>(theList: ListOf<T>) => {
    if (theList === "NILL")
        return 0
    else
        return theList.element + sumList(theList.rest)
}

console.log('Sum List:', sumList(myList))


// Exercise 4
const productList = <T extends number>(theList: ListOf<T>) => {
    if (theList === "NILL")
        return 1
    else
        return theList.element * productList(theList.rest)
}

console.log('Product List:', productList(myList))



// Exercise 5
const foldr = <T, TResult>(initialValue: TResult, f: (lhs: T, rhs: TResult)=>TResult,  theList: ListOf<T>): TResult => {
    if (theList === "NILL")
        return initialValue
    else
        return f(theList.element, foldr(initialValue, f, theList.rest))
}

const Add = (lhs: number,rhs: number): number => lhs+rhs
const Multiply = (lhs: number,rhs: number): number => lhs*rhs
console.log('Sum List - Exercise 5:', foldr(0, Add, myList))
console.log('Multiply List - Exercise 5:', foldr(1, Multiply, myList))


// Exercise 6
const list = <T>(values: T[]): ListOf<T> => {
    if (values.length == 0)
        return nil()
    else
        return cons(values[0], list(values.slice(1)))
} 
const myList2 = list([1,2,3,4,5]);
console.log('Sum List - Exercise 6:', foldr(0, Add, myList2))


// Exercise 6
const And = (lhs: boolean,rhs: boolean): boolean => lhs && rhs
const Or = (lhs: boolean,rhs: boolean): boolean => lhs || rhs
const allTrue = (values: ListOf<boolean>) => foldr(true, And, values);
const anyTrue = (values: ListOf<boolean>) => foldr(false, Or, values);

// Exercise
const copy = <T>(values: ListOf<T>) => foldr(nil(), cons, values);
const append = <T>(lhs: ListOf<T>, rhs: ListOf<T>) => foldr(rhs, cons, lhs)