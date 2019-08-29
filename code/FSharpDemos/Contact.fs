module Contact

open System
open NonEmptyString
open PositiveNumber
open Domain

[<NoEquality; NoComparison>]
type Contact = {
    id : Guid
    firstName : NonEmptyString
    lastName : NonEmptyString
    twitterProfileUrl : NonEmptyString option
    dateOfBirth : DateTime option
    primaryContactMethod : ContactMethod
    iq : PositiveNumber
 }

let create id firstName lastName twitterProfileUrl dateOfBirth primaryContactMethod iq =
    {
        Contact.id = id
        firstName = firstName
        lastName = lastName
        twitterProfileUrl = twitterProfileUrl
        dateOfBirth = dateOfBirth
        primaryContactMethod = primaryContactMethod
        iq = iq
    }
