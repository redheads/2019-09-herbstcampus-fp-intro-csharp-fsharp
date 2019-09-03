namespace FSharpDemos

open System
open FSharpPlus
open FSharpPlus.Data

module Contact =
    type Contact = {
        FirstName: string
        LastName: string
        DateOfBirth: DateTime option
        TwitterHandle: string option
    }
    
    type User = { FirstName: string }
    
    let create firstName lastName dateOfBirth twitterHandle =
        {
            Contact.FirstName = firstName
            LastName = lastName
            DateOfBirth = dateOfBirth
            TwitterHandle = twitterHandle
        }
        
    let stringIsNotEmpty s =
        if String.IsNullOrWhiteSpace s then
            Failure ["String must not be empty"]
        else
            Success s
    
    
    let validateAndCreate firstName lastName (dateOfBirth: DateTime option) (twitterHandle: string option) =
            (Success create)
            <*> (stringIsNotEmpty firstName)
            <*> (stringIsNotEmpty lastName)
            <*> (Success dateOfBirth)
            <*> (Success twitterHandle)
        
    let stringify contact =
        contact.ToString()
        
    let save (contact : Contact) : Result<Contact, string> =
        Ok contact
        
    let sendEmail (contact : Contact) =
        Error "Server unreachable"
        
    let output result =
        match result with
        | Ok contact -> stringify contact
        | Error err -> sprintf "something went wrong internally: %O" err
        
    let finalOutput (validation : Validation<string list, string>) =
        match validation with         
        | Failure errs -> String.Join(", ", errs)
        | Success s -> s        
        
    let workflow () =
        (validateAndCreate "Homer" "Simpson" None None)
        |> map save
        |> map (fun result -> Result.bind sendEmail result)
        |> map (fun result -> output result)
        |> finalOutput
       