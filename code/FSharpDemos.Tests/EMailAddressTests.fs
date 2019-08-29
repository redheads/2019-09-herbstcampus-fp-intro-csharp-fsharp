module EMailAddressTests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``valid Mail is Ok`` () =
    NonEmptyString.create "abc@def.de"
    |> Result.map (fun mail -> EmailAddress.create mail |> Result.isOkAndEquals EmailAddress.get (NonEmptyString.get mail) |> should equal true)

[<Fact>]
let ``invalid string becomes Error`` () =
    NonEmptyString.create "a..d"
    |> Result.map (fun mail -> EmailAddress.create mail |> Result.isError |> should equal true)