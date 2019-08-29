module Workflows

open System

let add guid firstName lastName twitterProfileUrl dateOfBirth
    primaryContactMethod iq = ()
// TODO
let delete guid = ()

let prepareOutput filterPredicate format makePrintable contacts =
    contacts
    |> List.filter filterPredicate
    |> List.map format
    |> List.reduce makePrintable
// let toDate (dt : DateTime) = dt.Date
// let x = Some DateTime.Now
// let y = Option.map toDate x
// let save =
//     Result.runExceptional (fun _ -> System.IO.File.Exists("abc"))
// let report (saved : Result<bool, string>) : Result<unit, string> =
// let a =
//     Result.bind (fun b -> sprintf "%O" b) z
