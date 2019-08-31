module FSharpWorkshop.Workflows

open FSharpPlus
open FSharpWorkshop
open FSharpWorkshop.Contact

let storeContact (contact : Contact) =
    contact
    |> Persistence.save
    |>> Contact.logContact
    >>= Notify.sendNotification
    |> Logging.output
    