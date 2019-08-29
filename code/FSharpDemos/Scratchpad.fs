module Scratchpad

// Zuweisungen statt Variablen - unveränderlich (Immutability)
let x = 3
// Rechte Seite immer eine Expression - Wert, Funktion, ...
//let add a b = a + b
let m = if 3 > 0 then 7 else 42

// Mutability nur auf Wunsch - normalerweise unnötig
let mutable y = 3
y <- 42


// der letzte Parameter kann mit dem Ergebnis der vorherigen Expression ausgefüllt werden
// "Pipe-Operator"
let double a = a * 2
4 |> double // ergibt 8
4|> double|> double // ergibt 16

// Discriminated Unions ("Tagged Union", "Sum Type", "Choice Type")
type Vehicle = | Bike | Car | Bus

// Pattern Matching zur Behandlung der verschiedenen Fälle
let vehicle = Bike
match vehicle with
| Bike -> "Ima ridin my bike"
| Car -> "Driving along in my automobile"
| Bus -> "SPEED"


// auch mit unterschiedlichen(!) Daten an jedem Fall möglich
type Shape =
    | Circle of float
    | Rectangle of float * float
let c = Circle 42.42
match c with
| Circle radius -> radius * radius * System.Math.PI
| Rectangle(width, height) -> width * height

type Product = unit

// Record Type
type ShoppingCart = {
    products : Product list
    total : float
    createdAt : System.DateTime
 }

// Typ muss bei Erzeugung normalerweise nicht angegeben werden - außer wenn es nicht eindeutig ist
let shoppingCart = {
    products = []
    total = 42.42
    createdAt = System.DateTime.Now
 }

// Typen werden automatisch abgeleitet sofern möglich
//let double a = a * 2 // int -> int

// Explizite Angaben möglich
let doubleExplicit (a : int) : int = a * 2

// int -> int -> int -> int
// eigentlich: int -> (int -> (int -> int))
let addThree a b c = a + b + c

// Partial Application
let add a b = a + b // int -> (int -> (int))
let add2 = add 2 // (int -> (int))
let six = add2 4 // (int)
let ten = add2 8

// Structural Equality
type Thing = { content : string; id : int }
let thing1 = { content = "abc"; id = 15 }
let thing2 = { content = "abc"; id = 15 }
let equal = (thing1 = thing2) // true

// NoEquality, NoComparison
[<NoEquality; NoComparison>]
type NonEquatableNonComparable = {
    Id : int
 }
