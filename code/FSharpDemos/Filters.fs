module Filters

open Contact

let byIq (removeBelow : int) (contact : Contact) : bool =
    (PositiveNumber.get contact.iq) >= removeBelow
