type PrependArgs<F extends (...args: any) => any, P extends any[]> =
  Parameters<F> extends [...P, ...infer R] ? (...args: R) => ReturnType<F> : never;

function partial<F extends (...args: any[]) => any, P extends any[]>(
  fn: F,
  ...presetArgs: P
): PrependArgs<F, P> {
  return ((...laterArgs: any[]) => fn(...presetArgs, ...laterArgs)) as PrependArgs<F, P>;
}


// Exercise 1 and 2
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


const productList = <T extends number>(theList: ListOf<T>) => {
    if (theList === "NILL")
        return 1
    else
        return theList.element * productList(theList.rest)
}

console.log('Product List:', productList(myList))



// Exercise 4
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


// Exercise 5
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
const allTrue = partial(foldr<boolean,boolean>, true, And);
const anyTrue = partial(foldr<boolean,boolean>, false, Or);

const myList3 = list([false,false,true,false]);
console.log('allTrue:', allTrue(myList3))
console.log('anyTrue:', anyTrue(myList3))

// Exercise 7
const copy = <T>(values: ListOf<T>) => foldr(nil(), cons, values);
const append = <T>(lhs: ListOf<T>, rhs: ListOf<T>) => foldr(rhs, cons, lhs)

const display = <T>(values: ListOf<T>) => {

    const inner = (values: ListOf<T>, acc: string): string => {
        if (values === 'NILL')
            return acc;
        else {
            if (acc === '')
                return inner(values.rest, values.element.toString())
            else    
                return inner(values.rest, acc + ',' + values.element)
        }
    }

    console.log(inner(values, ''))
}
display(myList3)
display(copy(myList3))
display(append(myList3, myList3))


// Exercise 8
const count = (_: any, cnt: number) => cnt + 1;
const listLength = partial(foldr<any,number>, 0, count);
console.log('listLength:', listLength(myList3))


// Exercise 9
const double = (a: number) => a * 2;
const doubleAll = partial(foldr<number,number>, 0, partial(cons<number>, double));
console.log('doubleAll:', doubleAll(myList))
