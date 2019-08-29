module GermanZipCodeTests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``valid Zip is Ok`` () =
    NonEmptyString.create "91056"
    |> Result.map (fun zip -> GermanZipCode.create zip |> Result.isOkAndEquals GermanZipCode.get (NonEmptyString.get zip) |> should equal true)

[<Fact>]
let ``short string becomes Error`` () =
    NonEmptyString.create "a..d"
    |> Result.map (fun zip -> GermanZipCode.create zip |> Result.isError |> should equal true)

[<Fact>]
let ``invalid string becomes Error`` () =
    NonEmptyString.create "91o56"
    |> Result.map (fun zip -> GermanZipCode.create zip |> Result.isError |> should equal true)