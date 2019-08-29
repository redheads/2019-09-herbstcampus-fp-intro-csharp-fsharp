module Option

let apply (f : ('a -> 'b) option) (a : 'a option) : 'b option =
    match f, a with
    | Some f, Some a -> Some(f a)
    | _, _ -> None
