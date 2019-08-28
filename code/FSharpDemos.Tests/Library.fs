
module Dummy

open FsUnit.Xunit
open Xunit
open FSharpDemos.Say


[<Fact>]
let ``Adding one works`` () =
    addOne 1 |> should equal 2 