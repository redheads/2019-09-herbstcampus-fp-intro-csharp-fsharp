module ContactMethodDto

open AddressDataDto
open Domain
open PostalAddress

type ContactMethodDto = {
    EmailAddress : string option
    PostalAddress : AddressDataDto option
 }

let fromDomain (domain : ContactMethod) : ContactMethodDto =
    match domain with
    | Email email -> { EmailAddress = Some <| EmailAddress.get email; PostalAddress = None }
    | Snailmail(PostalAddress postal) -> { EmailAddress = None; PostalAddress = Some <| AddressDataDto.fromDomain postal }

let toDomain (dto : ContactMethodDto) : Result<ContactMethod, string list> =
    match (dto.EmailAddress, dto.PostalAddress) with
    | Some email, _ ->
        NonEmptyString.create email
        |> Result.bind EmailAddress.create
        |> Result.map Email
    | _, Some postal ->
        AddressDataDto.toDomain postal
        |> Result.map (PostalAddress >> Snailmail)
    | _, _ -> Error [ "Contact Method is neither Email nor Postal" ]
