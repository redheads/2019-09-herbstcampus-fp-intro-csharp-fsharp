
module Dummy

open Xunit
open FSharpDemos.Say
open Swensen.Unquote

[<Fact>]
let ``Adding one works`` () =
    test <@ addOne 1 = 2 @>
