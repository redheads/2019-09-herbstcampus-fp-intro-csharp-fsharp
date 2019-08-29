module GermanZipCode

open System
open NonEmptyString

type GermanZipCode = private GermanZipCode of string

let (|GermanZipCode|) = function
    GermanZipCode s -> GermanZipCode s

let private checkLength s l =
    match String.length s = l with
    | true -> Ok s
    | false -> Error "string has wrong length"

let create (NonEmptyString s) =
    Result.map2 (fun _ _ -> GermanZipCode s) (checkLength s 5) (Util.tryResult (Int32.TryParse s))

let get (GermanZipCode s) =
    s