module ContactDto

open System
open Contact
open Domain
open ContactMethodDto
open PositiveNumber
open Result
open NonEmptyString

type ContactDto = {
    id : Guid
    firstName : string
    lastName : string
    twitterProfileUrl : string option
    dateOfBirth : DateTime option
    primaryContactMethod : ContactMethodDto
    iq : int
 }

let fromDomain (domain : Contact) : ContactDto =
    {
        id = domain.id
        firstName = NonEmptyString.get domain.firstName
        lastName = NonEmptyString.get domain.lastName
        twitterProfileUrl = Option.map NonEmptyString.get domain.twitterProfileUrl
        dateOfBirth = domain.dateOfBirth
        primaryContactMethod = ContactMethodDto.fromDomain domain.primaryContactMethod
        iq = PositiveNumber.get domain.iq
    }

let toDomain (dto : ContactDto) : Result<Contact, string list> =
    let resultTwitterProfileUrl =
        match dto.twitterProfileUrl with
        | None -> lift None
        | Some tpu -> NonEmptyString.create tpu |> Result.map Some

    (lift
        (Contact.create dto.id))
        <*> (NonEmptyString.create dto.firstName)
        <*> (NonEmptyString.create dto.lastName)
        <*> resultTwitterProfileUrl
        <*> (lift dto.dateOfBirth)
        <*> (ContactMethodDto.toDomain dto.primaryContactMethod)
        <*> (PositiveNumber.create dto.iq)
