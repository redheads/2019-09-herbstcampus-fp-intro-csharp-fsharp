module FSharpWorkshop.Tests.Helpers 

open FSharpPlus.Data

let isSuccess v =
    match v with
    | Success _ -> true
    | _ -> false
    
let isFailure v=
    not <| isSuccess v
    
let compareSuccessValue valueExtractorFn compareTo v =
    match v with
    | Failure _ -> false
    | Success value -> valueExtractorFn value = compareTo
        
         