module FSharpWorkshop.Notify

open FSharpWorkshop.Contact

let sendNotification (contact : Contact) =
    Ok contact