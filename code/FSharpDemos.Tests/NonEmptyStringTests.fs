module NonEmptyStringTests

open System
open Xunit
open FsUnit.Xunit

open NonEmptyString

[<Fact>]
let ``Returns Ok s with non-empty non-null s`` () =
    let s = "abc"
    create s |> Result.isOkAndEquals NonEmptyString.get s |> should equal true

[<Fact>]
let ``empty string becomes Error`` () =
    create "" |> Result.isError |> should equal true
    
[<Fact>]
let ``null string becomes Error`` () =
    create null |> Result.isError |> should equal true
