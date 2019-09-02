using System;
using System.Collections.Immutable;
using System.Linq;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace CSharpDemos
{
    public class AddressBook
    {
        public ImmutableList<Contact> Contacts { get; }

        public AddressBook(ImmutableList<Contact> contacts) => Contacts = contacts;

        public override string ToString() => $"AddressBook. Number of entries: {Contacts.Count}";
    }

//    public static class AddressBookExtensions
//    {
////        private static bool HasContact(this ImmutableList<Contact> contacts, Contact contact) 
////            => contacts.Any(x => x.Id == contact.Id);
////
////        private static bool HasContact(this AddressBook addressBook, Contact contact) 
////            => addressBook.Contacts.HasContact(contact);
////
////        private static AddressBook AddContact(this AddressBook addressBook, Contact contact) 
////            => new AddressBook(addressBook.Contacts.Add(contact));
////
////        private static AddressBook UpdateContact(this AddressBook addressBook, Contact contact)
////        {
////            var contactToReplace = addressBook.Contacts.Single(x => x.Id == contact.Id);
////            return new AddressBook(addressBook.Contacts.Replace(contactToReplace, contact));
////        }
//        
////        public static AddressBook AddOrUpdateContact(this AddressBook addressBook, Contact contact) 
////            => addressBook.HasContact(contact) 
////                ? addressBook.UpdateContact(contact) 
////                : addressBook.AddContact(contact);
//
////        public static AddressBook RemoveContact(this AddressBook addressBook, Contact contact) 
////            => new AddressBook(addressBook.Contacts.Remove(contact));
//    }

    public static class AddressBookFunctionalExtensions
    {
        // NOTE: This function can be passed to Bind
        public static Either<string, AddressBook> AddOrUpdateContactOpt(this AddressBook addressBook, Contact contact)
        {
            var numberOfMatchesFound = addressBook.Contacts.Count(x => x.Id == contact.Id);
            switch (numberOfMatchesFound)
            {
                case 0:
                    return Right(new AddressBook(addressBook.Contacts.Add(contact)));
                case 1:
                    return Right(new AddressBook(addressBook.Contacts.Replace(
                        addressBook.Contacts.Single(x => x.Id == contact.Id),
                        contact)));
                default:
                    return Left($"There where {numberOfMatchesFound} matches found for id {contact.Id}!");
            }
        }

        public static Either<string, AddressBook> RemoveOpt(this AddressBook addressBook, Contact contact) 
            => Right(new AddressBook(addressBook.Contacts.Remove(contact)));

        public static AddressBook UnsafeUnpack(this Either<string, AddressBook> addressBookOpt) =>
            addressBookOpt.Match(
                _ => throw new Exception("No address book present!"),
                x => x);
    }
}