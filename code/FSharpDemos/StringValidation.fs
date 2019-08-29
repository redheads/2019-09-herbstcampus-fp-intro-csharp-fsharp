module StringValidation

open System

let isNotNull s =
    match String.IsNullOrWhiteSpace s with
    | true -> Error "String must not be null"
    | false -> Ok s

let hasMaxLength (s : string) =
    match s.Length > 5 with
    | true -> Error "String must not be longer than 5"
    | false -> Ok s

let toInt (s : string) =
    match Int32.TryParse(s) with
    | (true, num) -> Ok num
    | (false, _) -> Error "Could not be parsed to a number"
