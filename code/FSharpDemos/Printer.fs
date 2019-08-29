module Printer

open NonEmptyString

let reduceToSingleString (nonEmptyString1 : NonEmptyString)
    (nonEmptyString2 : NonEmptyString) =
    NonEmptyString.map (sprintf "%s,%s") nonEmptyString1 nonEmptyString2
