module Result

open FsUnit.Xunit

let isOkAndEquals getFn compareTo result =
    match result with
    | Error _ -> false
    | Ok v -> getFn v = compareTo

let isError result =
    match result with
    | Error _ -> true
    | Ok _ -> false

let fail = true |> should equal false
