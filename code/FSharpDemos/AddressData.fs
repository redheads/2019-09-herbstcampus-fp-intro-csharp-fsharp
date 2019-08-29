module AddressData

open NonEmptyString
open GermanZipCode

type AddressData = {
    street : NonEmptyString
    zip : GermanZipCode
    city : NonEmptyString
 }

let create street zip city =
    {
        street = street
        zip = zip
        city = city
    }
