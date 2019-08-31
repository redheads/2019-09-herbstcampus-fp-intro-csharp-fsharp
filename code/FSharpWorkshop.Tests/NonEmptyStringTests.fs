module FSharpWorkshop.Tests.NonEmptyStringTests

open Xunit
open Swensen.Unquote
open FSharpWorkshop.NonEmptyString
open FSharpWorkshop.Tests.Helpers
open FSharpPlus.Data

[<Fact>]
let ``an empty string results in a validation Failure`` () =
    test <@  isFailure (create "") = true @>
    
[<Fact>]
let ``a null string results in a validation Failure`` () =
    test <@  isFailure (create null) = true @>
    
[<Fact>]
let ``a valid string results in a validation Success`` () =
    test <@ isSuccess (create "Harry") = true @>
    
[<Fact>]
let ``a valid NonEmptyString contains the correct original string`` () =
    let orig = "Harry"
    test <@  (compareSuccessValue FSharpWorkshop.NonEmptyString.get orig (create orig)) = true @>