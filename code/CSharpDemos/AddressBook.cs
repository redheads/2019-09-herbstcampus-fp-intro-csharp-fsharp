using System.Collections.Immutable;
using System.Linq;

namespace CSharpDemos
{
    public class AddressBook
    {
        public ImmutableList<Contact> Contacts { get; }

        public AddressBook(ImmutableList<Contact> contacts) => Contacts = contacts;
    }

    public static class AddressBookExtensions
    {
        private static bool HasContact(this ImmutableList<Contact> contacts, Contact contact) 
            => contacts.Any(x => x.Id == contact.Id);

        private static bool HasContact(this AddressBook addressBook, Contact contact) 
            => addressBook.Contacts.HasContact(contact);

        private static AddressBook AddContact(this AddressBook addressBook, Contact contact) 
            => new AddressBook(addressBook.Contacts.Add(contact));

        private static AddressBook UpdateContact(this AddressBook addressBook, Contact contact)
        {
            var contactToReplace = addressBook.Contacts.Single(x => x.Id == contact.Id);
            return new AddressBook(addressBook.Contacts.Replace(contactToReplace, contact));
        }
        
        public static AddressBook AddOrUpdateContact(this AddressBook addressBook, Contact contact) 
            => addressBook.HasContact(contact) 
                ? addressBook.UpdateContact(contact) 
                : addressBook.AddContact(contact);
    }
}