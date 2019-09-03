using System;
using CSharpDemos.ValueObjects;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace CSharpDemos
{
    public class Contact
    {
        private Contact(Id id, NonEmptyString firstName, NonEmptyString lastName, 
            Option<DateOfBirth> dateOfBirth, Option<NonEmptyString> twitterHandle)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            TwitterHandle = twitterHandle;
        }

        private static readonly Func<Id, NonEmptyString, NonEmptyString, Option<DateOfBirth>, Option<NonEmptyString>, Contact> 
            Create
            = (id, firstName, lastName, optDob, optTwitter) 
                => new Contact(id, firstName, lastName, optDob, optTwitter);

        // Not sure if this function should be moved somewhere else
        public static Validation<Contact> CreateValidContact(Option<Id> optId, Option<NonEmptyString> optFirstName,
            Option<NonEmptyString> optLastName, Option<DateOfBirth> optDob,
            Option<NonEmptyString> optTwitterHandle)
        {
            Validation<Id> ValidateId(Option<Id> opt) 
                => opt.Match(
                    () => Error("invalid Id"), 
                    x => Valid(x));

//            Func<Option<Id>, Validation<Id>> ValidateId2 
//                = opt => opt.Match(
//                    () => Error("invalid Id"), 
//                    x => Valid(x));

            Validation<NonEmptyString> ValidateFirstName(Option<NonEmptyString> opt) 
                => opt.Match(
                    () => Error("invalid FirstName"), 
                    s => Valid(s));

            Validation<NonEmptyString> ValidateLastName(Option<NonEmptyString> opt) 
                => opt.Match(
                    () => Error("invalid LastName"), 
                    s => Valid(s));

            return Valid(Create)
                .Apply(ValidateId(optId))
                .Apply(ValidateFirstName(optFirstName))
                .Apply(ValidateLastName(optLastName))
                .Apply(optDob)
                .Apply(optTwitterHandle);
        }
        
        public Id Id { get; }
        public NonEmptyString FirstName { get; }
        public NonEmptyString LastName { get; }
        public Option<DateOfBirth> DateOfBirth { get; }
        public Option<NonEmptyString> TwitterHandle { get; }
        
        public override string ToString() => $"Id: {Id}: {FirstName} {LastName} (DOB: {DateOfBirth}, Twitter: {TwitterHandle})";
    }
}