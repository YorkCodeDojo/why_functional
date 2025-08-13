// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

// let x = ()
// 
// let a:  Option<string> = Some("HellO)")
// let b: Option<string> = None
// 

// Option<String> a = Some("Hello")


// Write a function Nil() which returns an empty list.

  // type NIL = unit

  // let myEmptyList : NIL = ()

  // printfn "%O" myEmptyList


// type C = C * C | unit
// type E<'a> = Choice1 of 'a | Choice2 of 'a * 'a
// type A = int * int
// type B = {FirstName:string; LastName:string}

// type B = {head: int; tail:B} | NIL
// type B = int * (Value of B) | NIL
// type A = {head: int; tail:B}


// type Gift =
// | Book of Book
// | Chocolate of Chocolate
// | Wrapped of Gift * WrappingPaperStyle
// | Boxed of Gift
// | WithACard of Gift * message:string
// 

// type A = | Item of head:int * A | Empty
type ListOf<'T> = | Cons of head:'T * ListOf<'T> | Empty

let x =  Empty // An empty list 
let y =  Cons(1, Empty) // Single item in list
let z =  Cons(1, Cons(2, Empty)) // Two item in list

printfn "x=%O" x

printfn "y=%O" y
printfn "z=%O" z

let nil() = Empty
let createTwoItemList a b = Cons(a, Cons(b, Empty))

let a = createTwoItemList 1 2
printfn "a=%O" a

let isNil<'T>(list: ListOf<'T>): bool= 
  match list with
  | Empty -> true
  | _ -> false

let head<'T>(list: ListOf<'T>): Option<'T> = 
  match list with
  | Cons( head:'T , _) -> Some(head)
  | _ -> None

let tail<'T>(list: ListOf<'T>): ListOf<'T> = 
  match list with
  | Cons( head:'T , tail) -> tail
  | Empty -> Empty

//Cons of head:'T * ListOf<'T>
// let head2<'T>(list: Cons) : 'T = 
//   match list with
//   | Cons( head:'T , _) -> Some(head)
//   | _ -> None

let sum(list: ListOf<int>): int = 
  let rec sumList(list: ListOf<int>, total: int): int = 
    match head(list) with
    | Some(x) -> sumList(tail(list), total + x)
    | None -> total

  sumList(list, 0)

let rec fold(list: ListOf<int>)(acc: int)(folder: (int*int)->int ): int =
  match head(list) with
  | Some(x) -> fold(tail(list))(folder(acc , x))(folder)
  | None -> acc


printfn "isNil for empty list is %O" (isNil(x))
printfn "isNil for non empty list is %O" (isNil(y))

printfn "head for a list with stuff in %O" (head(y))

printfn "tail for a list with stuff in %O" (tail(z))
printfn "tail for a list which is empty %O" (tail(x))

printfn "Sum a list = %O" (sum(z))

printfn "Fold  a list = %O" (fold(z)(0)(fun (a,b) -> a + b))
