module Result

let map2 f a b =
    match a, b with
    | Ok a, Ok b -> Ok(f a b)
    | Error a, Error b -> Error [ a; b ]
    | Error a, _ -> Error [ a ]
    | _, Error b -> Error [ b ]

let apply (f : Result<'a -> 'b, 'c list>) (a : Result<'a, 'c list>) : Result<'b, 'c list> =
    match f, a with
    | Ok f, Ok a -> Ok(f a)
    | Error f, Error a -> Error <| f @ a
    | Error f, _ -> Error f
    | _, Error a -> Error a

let flatMap = Result.bind
let lift a = Ok a
let (<!>) = Result.map
let (<*>) = apply
let (>>=) = Result.bind
let map3 f a b c = f <!> a <*> b <*> c

let runExceptional fn =
    try
        Ok(fn())
    with ex -> Error ex.Message
