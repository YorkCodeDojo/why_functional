type ListOf<'T> =
    | Nil
    | ListValues of head:'T * body:ListOf<'T>

let cons element list: ListOf<'T> = ListValues ( element, list )

let nil<'T>: ListOf<'T> = Nil

// Exercise 3
let myList = cons 1 (cons 2 (cons 4 nil))

let rec sumList (values: ListOf<int>): int =
    match values with
    | Nil -> 0
    | ListValues (head,rest) -> head + sumList(rest)

printfn $"%d{sumList(myList)}"

let rec multiplyList (values: ListOf<int>): int =
    match values with
    | Nil -> 1
    | ListValues (head,rest) -> head * multiplyList(rest)
    
printfn $"%d{multiplyList(myList)}"

let rec foldr<'T, 'TResult> (acc: 'TResult) (f: 'T -> 'TResult -> 'TResult) (values: ListOf<'T>): 'TResult =
    match values with
    | Nil -> acc
    | ListValues (head,rest) -> f head (foldr acc f rest)
    
let summer = foldr 0 (+)
printfn $"%d{summer(myList)}"