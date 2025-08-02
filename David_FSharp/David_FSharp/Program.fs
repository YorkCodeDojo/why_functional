type ListOf<'T> =
    | Nil
    | ListValues of head:'T * body:ListOf<'T>

let cons element list: ListOf<'T> = ListValues ( element, list )

let nil<'T>: ListOf<'T> = Nil


let rec listToString values =
    listToString' values "["
and listToString' values acc =
    match values with
    | Nil -> acc
    | ListValues (head,Nil) -> $"{acc}{head}]"
    | ListValues (head,rest) -> listToString' rest $"{acc}{head}, "

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
    
let sum = foldr 0 (+)
printfn $"%d{sum(myList)}"

let product = foldr 1 (*)
printfn $"%d{product(myList)}"


// Exercise 5
let rec makeList (values: list<'T>): ListOf<'T> =
    match values with
    | [] -> nil
    | head :: tail -> cons head (makeList tail)

let myLongList = makeList [ 1; 2; 3; 4; 5; 6; 7; 8; 9; ]
printfn $"%d{sum(myLongList)}"


// Exercise 6
let allTrue = foldr true (&&)
let anyTrue = foldr false (||)

let booleanList = makeList [ true; false; true ]
printfn $"%s{allTrue(booleanList).ToString()}"
printfn $"%s{anyTrue(booleanList).ToString()}"


// Exercise 7
let copy = foldr Nil cons
let append lhs rhs = foldr rhs cons lhs
printfn $"%d{sum(copy(myLongList))}"

let newList = append myLongList myLongList
printfn $"%d{sum(newList)}"



// Exercise 8
let count _ rhs = rhs + 1
let length = foldr 0 count

printfn $"%d{length(newList)}"


// Exercise 9
let doubleThenCons n = cons (n*2)
let doubleAll = foldr Nil doubleThenCons

let smallList = makeList [ 10; 25 ]
printfn $"%d{smallList |> doubleAll |> sum }" // 20+50=70


// Exercise 10
let double n = n * 2
let map f = foldr Nil (f >> cons)
let doubleAll' = map double
printfn $"%d{smallList |> doubleAll' |> sum }" // 20+50=70


// Exercise 11 ---  Trees
type TreeOf<'T> = {
    label: 'T
    subTrees: ListOf<TreeOf<'T>>
}

let node label subTrees = { label = label; subTrees = subTrees}

let myTree = node 1 (makeList [
    node 2 nil;
    node 3
        (cons (node 4 nil) nil)
])

let rec foldTree f g a (root: TreeOf<'T>) =
    f root.label (foldTree' f g a root.subTrees)
and foldTree' f g a (subTrees: ListOf<TreeOf<'T>>) = 
    match subTrees with
    | Nil -> a
    | ListValues (subTree, rest) -> g (foldTree f g a subTree) (foldTree' f g a rest)
    
    
let sumTree = foldTree (+) (+) 0
printfn $"%d{myTree |> sumTree }" // 1+2+3+4=10

let labels = foldTree cons append Nil
printfn $"%s{myTree |> labels |> listToString}" // 1,2,3,4


let mapTree f = foldTree (f >> node) cons Nil
let doubleTree = mapTree double

printfn $"%s{myTree |> doubleTree |> labels |> listToString}" // 2,4,6,8