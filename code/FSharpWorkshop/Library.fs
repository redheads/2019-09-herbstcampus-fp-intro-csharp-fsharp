namespace FSharpDemos

open System
open FSharpPlus
open FSharpPlus.Data

module Contact =
    type Contact =
        { FirstName : string
          LastName : string
          DateOfBirth : DateTime option
          TwitterHandle : string option }

    type User =
        { FirstName : string }

    let create firstName lastName dateOfBirth twitterHandle =
        { Contact.FirstName = firstName
          LastName = lastName
          DateOfBirth = dateOfBirth
          TwitterHandle = twitterHandle }

    let stringIsNotEmpty (s : string) : Validation<string list, string> =
        if String.IsNullOrWhiteSpace s then
            Failure [ "String must not be empty" ]
        else Success s

    let validateAndCreate firstName lastName (dateOfBirth : DateTime option)
        (twitterHandle : string option) : Validation<string list, Contact> =
        (Success create) <*> (stringIsNotEmpty firstName)
        <*> (stringIsNotEmpty lastName) <*> (Success dateOfBirth)
        <*> (Success twitterHandle)
    let stringify (contact : Contact) : string = contact.ToString()
    let save (contact : Contact) : Result<Contact, string> = Ok contact
    let sendEmail (contact : Contact) : Result<Contact, string> =
        Error "Server unreachable"

    let output (result : Result<Contact, string>) : string =
        match result with
        | Ok contact -> stringify contact
        | Error err -> sprintf "something went wrong internally: %O" err

    let finalOutput (validation : Validation<string list, string>) : string =
        match validation with
        | Failure errs -> String.Join(", ", errs)
        | Success s -> s

    let workflow() =
        (validateAndCreate "Homer" "Simpson" None None)
        |> map save
        |> map (Result.bind sendEmail)
        |> map output
        |> finalOutput

    // we have to use |>> because of the argument order:
    // From FSharpPlus documentation:
    // "( |>> ) x f     Lift a function into a Functor. Same as map but with flipped arguments. To be used in pipe-forward style expressions"
    // In contrast the <!> has the signature: ( <!> ) f x
    let workflowInfix() : string =
        (validateAndCreate "Homer" "Simpson" None None) 
        |>> save
        |>> (Result.bind sendEmail) 
        |>> output 
        |> finalOutput
