// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

// let x = ()
// 
let a:  Option<string> = Some("HellO)")
let b: Option<string> = None
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
type A<'T> = | Item of head:'T * A<'T> | Empty


let x =  Empty // An empty list 
let y =  Item(8, Empty) // Single item in list
let z =  Item(1, Item(2, Empty)) // Two item in list

printfn "x=%O" x

printfn "y=%O" y
printfn "z=%O" z

