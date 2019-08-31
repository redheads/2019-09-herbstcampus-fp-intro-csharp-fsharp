module FSharpWorkshop.Contact
    open System
    open FSharpPlus
    open FSharpPlus.Data
    open NonEmptyString

    type DateWithoutTime = DateTime

    type Contact = {
        vorname: NonEmptyString
        nachname: NonEmptyString
        geburtstag: DateWithoutTime option
        anmerkung: NonEmptyString option
    }
    
    let create vorname nachname geburtstag anmerkung = 
        {
            vorname = vorname
            nachname = nachname
            geburtstag = geburtstag
            anmerkung = anmerkung
            
        }
        
    let validate vorname nachname geburtstag anmerkung : Validation<string list, Contact> =
        create <!> (NonEmptyString.create vorname)
            <*> (NonEmptyString.create nachname)
            <*> (Success geburtstag)
            <*> (map Some (NonEmptyString.create anmerkung))
                      
        

    let printGeburtstag (dt : DateWithoutTime) =
        dt.ToLongDateString()
        

    let printGeburtstagOption (dt : DateWithoutTime option) =
        map printGeburtstag dt
        
    let removeTime (dt: DateTime) : DateWithoutTime =
        dt.Date
        
    let removeTimeOption (dt: DateTime option) : DateWithoutTime option =
        map removeTime dt
    
    let logContact (contact: Contact) =
        printfn "Contact: %O" contact
        contact