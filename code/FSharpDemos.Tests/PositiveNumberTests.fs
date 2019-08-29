module PositiveNumberTests

open Xunit
open FsUnit.Xunit

[<Fact>]
let ``positive number is Ok`` () =
    PositiveNumber.create 42 |> Result.isOkAndEquals PositiveNumber.get 42 |> should equal true

[<Fact>]
let ``0 becomes Error`` () =
    PositiveNumber.create 0 |> Result.isError |> should equal true

[<Fact>]
let ``negative number becomes Error`` () =
    PositiveNumber.create -1234 |> Result.isError |> should equal true