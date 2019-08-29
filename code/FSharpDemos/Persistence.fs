module Persistence

open System
open System.IO
open Contact
open ContactDto
open Result

let createEmptyFileIfNoFileIsPresent writeFile isFilePresent filePath =
    if isFilePresent filePath then ()
    else
        let emptyJsonArray = "[]"
        writeFile filePath emptyJsonArray

let changeFileContent readFile writeFile isFilePresent filePath
    (changeFn : ContactDto list -> Result<ContactDto list, string list>) =

    try
        createEmptyFileIfNoFileIsPresent writeFile isFilePresent filePath
        readFile
        |> Newtonsoft.Json.JsonConvert.DeserializeObject<ContactDto list>
        |> changeFn
        |> Result.map Newtonsoft.Json.JsonConvert.SerializeObject
        |> Result.map (writeFile filePath)
    with
        ex -> Error [ ex.Message ]

let addNewContactToList (contact : Result<Contact, 'a list>) : ContactDto list -> Result<ContactDto list, 'a list>
        =
        let addNewElementToExistingList (newEle : Result<ContactDto, 'b list>) oldList =
                Result.map (fun e -> e :: oldList) newEle

        Result.map ContactDto.fromDomain contact
        |> addNewElementToExistingList

let add readFile writeFile isFilePresent filePath (contact : Contact) : Result<unit, string list> =
    changeFileContent readFile writeFile isFilePresent filePath
        (addNewContactToList (Ok contact))

let editContact oldContactId changed oldContacts =
        let errorIfNoOldEntryWasRemoved filteredContacts =
                if List.length filteredContacts = List.length oldContacts then
                        Error [ "Contact to edit not found" ]
                else
                        Ok filteredContacts
        let compareById (c : ContactDto) =
                c.id = oldContactId

        List.filter compareById oldContacts
        |> List.except oldContacts
        |> errorIfNoOldEntryWasRemoved
        |> Result.bind (addNewContactToList (Ok changed))

let edit readFile writeFile isFilePresent filePath oldContactId (changedContact : Contact) : Result<Contact, string list> =
        let editContact' = (editContact oldContactId changedContact)

        changeFileContent readFile writeFile isFilePresent filePath editContact'
        |> Result.map (fun _ -> changedContact)




let getFilePath basePath = Path.Combine [| basePath; "addressbook.json" |]
let getBasePath() = Directory.GetCurrentDirectory()
let getPath = getBasePath >> getFilePath
let writeToFile path contents = File.WriteAllText(path, contents)
let readFromFile path = File.ReadAllText path
let isFilePresent filePath = File.Exists filePath
