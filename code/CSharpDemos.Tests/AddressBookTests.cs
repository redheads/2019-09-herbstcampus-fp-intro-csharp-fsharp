using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using Xunit;
using static LaYumba.Functional.F;

namespace CSharpDemos.Tests
{
    public class AddressBookTests
    {
        private static Contact CreateSampleContact(int id, string firstName, string lastName, DateTime? dob = null, string twitter = null)
        {
            var optId = Id.Create(id);
            var optFirstName = NonEmptyString.Create(firstName);
            var optLastName = NonEmptyString.Create(lastName);
            var optDob = dob != null ? Some(dob.Value) : None;
            var optTwitterHandle = twitter != null ? NonEmptyString.Create(twitter) : None;

            var validContact = Contact.CreateValidContact(optId, optFirstName, optLastName, optDob, optTwitterHandle);
            return validContact.Match(_ => null, x => x);
        }

        [Fact]
        public void Adding_contacts_increments_entries_in_address_book()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var marge = CreateSampleContact(2, "Marge", "Simpson");
            
            // Act
            var addressBook1 = emptyAddressBook.AddOrUpdateContact(homer);
            var addressBook2 = addressBook1.AddOrUpdateContact(marge);
            
            // Assert
            addressBook2.Contacts.Should().HaveCount(emptyAddressBook.Contacts.Count + 2);
        }

        [Fact]
        public void Updating_contact_in_address_book()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var homerWithTwitter = CreateSampleContact(1, "Homer", "Simpson", null, "@homer");
            
            // Act
            var addressBook1 = emptyAddressBook.AddOrUpdateContact(homer);
            var addressBook2 = addressBook1.AddOrUpdateContact(homerWithTwitter);
            
            // Assert
            addressBook2.Contacts.Should().HaveCount(emptyAddressBook.Contacts.Count + 1);
            var contact = addressBook2.Contacts.First();
            contact.Id.Value.Should().Be(1);
            contact.TwitterHandle.ToString().Should().Be("Some(@homer)");
        }

        [Fact]
        public void Deleting_contact_from_address_book_decrements_entries()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var marge = CreateSampleContact(2, "Marge", "Simpson");
            var addressBook1 = emptyAddressBook.AddOrUpdateContact(homer);
            var addressBook2 = addressBook1.AddOrUpdateContact(marge);
            
            // Act
            var addressBook3 = addressBook2.RemoveContact(homer);
            
            // Assert
            addressBook3.Contacts.Should().HaveCount(1);
            addressBook3.Contacts.First().Id.Value.Should().Be(2);
        }
    }
}