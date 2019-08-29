namespace FSharpDemos

open FSharpPlus

module Say =
    let hello name =
        printfn "Hello %s" name

    let addOne n = n + 1