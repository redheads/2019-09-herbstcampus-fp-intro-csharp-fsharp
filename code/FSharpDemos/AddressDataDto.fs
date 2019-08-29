module AddressDataDto

open AddressData

type AddressDataDto = {
    street : string
    zip : string
    city : string
 }

let fromDomain (addressData : AddressData) : AddressDataDto =
    {
        street = NonEmptyString.get addressData.street
        zip = GermanZipCode.get addressData.zip
        city = NonEmptyString.get addressData.city
    }

let toDomain (dto : AddressDataDto) : Result<AddressData, string list> =
    Result.map3 AddressData.create (NonEmptyString.create dto.street) (dto.zip |> NonEmptyString.create |> Result.bind GermanZipCode.create) (NonEmptyString.create dto.city)
