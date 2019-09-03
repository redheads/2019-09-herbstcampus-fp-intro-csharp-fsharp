module TestContacts

open Domain
open Result

let homer =
        (lift
             (Contact.create
              <| System.Guid.Parse("bba52030-19ce-4c02-b1dd-792b0120855b")))
        <*> (NonEmptyString.create "Homer")
        <*> (NonEmptyString.create "Simpson")
        <*> (lift None) 
        <*> (lift None)
        <*> (Result.map Email
                 (Result.bind EmailAddress.create
                      (NonEmptyString.create "a@b.c")))
        <*> (PositiveNumber.create 50)

let lisa =
    (lift
         (Contact.create
          <| System.Guid.Parse("9af01860-8bb0-4e54-a6bb-2c7614fef928")))
    <*> (NonEmptyString.create "Lisa") <*> (NonEmptyString.create "Simpson")
    <*> (lift None) <*> (lift None)
    <*> (Result.map Email
             (Result.bind EmailAddress.create
                  (NonEmptyString.create "a@b.c")))
    <*> (PositiveNumber.create 120)

let bart =
    (lift
         (Contact.create
          <| System.Guid.Parse("bba52030-19ce-4c02-b1dd-792b0120855b")))
    <*> (NonEmptyString.create "Bart") <*> (NonEmptyString.create "Simpson")
    <*> (lift None) <*> (lift None)
    <*> (Result.map Email
             (Result.bind EmailAddress.create
                  (NonEmptyString.create "a@b.c")))
    <*> (PositiveNumber.create 90)
