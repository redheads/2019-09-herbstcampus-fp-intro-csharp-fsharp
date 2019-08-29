module Util

let tryResult f =
    match  f  with
        | true, value -> Ok value
        | _ -> Error "Can't parse input"
