module EmailAddress

open NonEmptyString

type EmailAddress = private EmailAddress of string

let (|EmailAddress|) = function
        | EmailAddress s -> EmailAddress s

let create (nes : NonEmptyString) =
    try
        let (NonEmptyString s) = nes
        System.Net.Mail.MailAddress(s, null) |> ignore
        Ok <| EmailAddress s

    with
        | :? System.FormatException as ex -> Error ["Invalid format"]
        | _ -> Error ["Other unexpected error"]

let get (EmailAddress ea) = 
    ea