module FSharpWorkshop.Logging

let output (r: Result<'a, 'b>) =
    match r with
        | Ok _ -> printfn "all okay!"
        | Error err -> printfn "Error: %O" err
