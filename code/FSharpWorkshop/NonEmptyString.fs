module FSharpWorkshop.NonEmptyString

open System
open FSharpPlus.Data

type NonEmptyString = private NonEmptyString of string

let (|NonEmptyString|) = function
    | NonEmptyString s -> NonEmptyString s
    
let create s =
    if (String.IsNullOrWhiteSpace s) then
        Failure ["Input string must contain something"]
    else
        Success (NonEmptyString s)

let get (NonEmptyString nes) = nes
    