module WorkflowsTests

open Xunit
open FsUnit.Xunit
open TestContacts

[<Fact>]
let ``prepareOutput function works``() =
    // Idee: create mit der Guid angewendet via Lift in ein Result, alles andere auch liften was kein Result ist,
    // und dann mit apply alles verbinden.
    // Dann kommt am Ende entweder ein Contact raus, oder eine Liste mit Errors, was nicht geklappt hat


    match homer, lisa, bart with
    | Ok h, Ok l, Ok b ->
        [ h; l; b ]
        |> (Workflows.prepareOutput (Filters.byIq 60) Formatters.firstNameOnly
                Printer.reduceToSingleString)
        |> NonEmptyString.get
        |> (should equal "Lisa,Bart")
    | _, _, _ -> true |> should equal false
