module PositiveNumber

type PositiveNumber = private PositiveNumber of int

let (|PositiveNumber|) = function
        | PositiveNumber i -> PositiveNumber i

let create (num : int) =
    if num > 0 then
        Ok <| PositiveNumber num
    else
      Error <| ["Number must be greater than 0"]

let get (PositiveNumber pn) = 
    pn