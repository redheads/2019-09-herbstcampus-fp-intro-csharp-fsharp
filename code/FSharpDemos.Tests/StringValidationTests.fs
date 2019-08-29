module StringValidationTests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``String validation combined workflow works``() =
    "4242"
    |> StringValidation.isNotNull
    |> Result.bind StringValidation.hasMaxLength
    |> Result.bind StringValidation.toInt
    |> Result.isOkAndEquals id 4242
    |> should equal true

[<Theory>]
[<InlineData("a", "Could not be parsed to a number")>]
[<InlineData(null, "String must not be null")>]
[<InlineData("123456", "String must not be longer than 5")>]
let ``String validation combined workflow works with invalid input`` (input, expected) =
    let result =
        input
        |> StringValidation.isNotNull
        |> Result.bind StringValidation.hasMaxLength
        |> Result.bind StringValidation.toInt

    match result with
    | Ok _ -> true |> should equal false
    | Error msg -> msg |> should equal expected
