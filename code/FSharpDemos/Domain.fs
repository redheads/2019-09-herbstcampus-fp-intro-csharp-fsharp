module Domain

open EmailAddress
open PostalAddress

type ContactMethod =
    | Email of EmailAddress
    | Snailmail of PostalAddress 
