module PersistenceTests

open System
open Xunit
open FsUnit.Xunit
open TestContacts

open Persistence
open Result

let existingHomerJson = """[{"id":"bba52030-19ce-4c02-b1dd-792b0120855b","firstName":"Homer","lastName":"Simpson","twitterProfileUrl":null,"dateOfBirth":null,"primaryContactMethod":{"EmailAddress":{"Case":"Some","Fields":["a@b.c"]},"PostalAddress":null},"iq":50}]"""

[<Fact>]
let ``adding an entry to an empty 'file' adds the entry``() =
    let readFile = "[]"
    let writeFile p s =
        ()
    let isFilePresent p = true
    let filePath = "dontcare"

    let fn = Persistence.add readFile writeFile isFilePresent filePath
    match Result.map fn homer with
    | Ok _ -> true |> should equal true
    | Error _ -> fail

[<Fact>]
let ``adding an entry to existing entries adds the entry``() =
    let mutable finalString = ""
    let readFile = existingHomerJson
    let writeFile p s =
        finalString <- s
        ()
    let isFilePresent p = true
    let filePath = "dontcare"

    let fn = Persistence.add readFile writeFile isFilePresent filePath
    match Result.map fn lisa with
    | Ok _ -> (String.length finalString) |> should be (greaterThan (String.length readFile))
    | Error _ -> fail

[<Fact>]
let ``editing a non-existing entry results in an Error``() =
    let readFile = "[]"
    let writeFile p s =
        ()
    let isFilePresent p = true
    let filePath = "dontcare"

    match (homer, lisa) with
    | (Ok homer, Ok lisa) ->
        match (Persistence.edit readFile writeFile isFilePresent filePath homer.id lisa) with
        | Ok _ -> fail
        | Error err -> err |> should equal [ "Contact to edit not found" ]
    | _ -> fail

[<Fact>]
let ``editing an existing entry results in a changed entry``() =
    let mutable finalString = ""
    let readFile = existingHomerJson
    let writeFile p s =
        finalString <- s
        ()
    let isFilePresent p = true
    let filePath = "dontcare"

    match (homer, PositiveNumber.create 150) with
    | (Ok homer, Ok newIq) ->
        let newHomer = { homer with iq = newIq }
        match (Persistence.edit readFile writeFile isFilePresent filePath homer.id newHomer) with
        | Ok nc ->
            nc |> should equal newHomer
            finalString |> should haveSubstring "150"
        | _ ->
            fail
    | _ -> fail


