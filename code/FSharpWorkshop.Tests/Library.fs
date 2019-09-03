module Dummy

open Xunit
open FSharpDemos.Contact
open Swensen.Unquote

[<Fact>]
let ``The banana is real`` () =
    test <@ banana = "BANANA" @>
