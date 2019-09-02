using System;
using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
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
            var optDob = dob != null ? Some(new DateOfBirth(dob.Value)) : None;
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
            var addressBook = emptyAddressBook
                .AddOrUpdateContact(homer)
                .AddOrUpdateContact(marge);
            
            // Assert
            addressBook.Contacts.Should().HaveCount(emptyAddressBook.Contacts.Count + 2);
        }

        [Fact]
        public void Updating_contact_in_address_book()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var homerWithTwitter = CreateSampleContact(1, "Homer", "Simpson", null, "@homer");
            
            // Act
            var addressBook = emptyAddressBook
                .AddOrUpdateContact(homer)
                .AddOrUpdateContact(homerWithTwitter);
            
            // Assert
            addressBook.Contacts.Should().HaveCount(emptyAddressBook.Contacts.Count + 1);
            var contact = addressBook.Contacts.First();
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
            var addressBook = emptyAddressBook
                .AddOrUpdateContact(homer)
                .AddOrUpdateContact(marge);
            
            // Act
            var addressBook2 = addressBook.RemoveContact(homer);
            
            // Assert
            addressBook2.Contacts.Should().HaveCount(1);
            addressBook2.Contacts.First().Id.Value.Should().Be(2);
        }
//
//        [Fact]
//        public void Foo()
//        {
//            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
//            var homer = CreateSampleContact(1, "Homer", "Simpson");
//
////            var result = Right(emptyAddressBook)
////                .Bind(x => AddressBookFunctional.AddOrUpdateContact(x, homer))
////                .Bind(x => AddressBookFunctional.SendConfirmationMail(new Mailer(), homer, x));
//
//            Func<Contact, Either<string, Contact>> mailer = c => Right(c);
//            
//            var result = Right(emptyAddressBook)
//                .Bind(x => AddressBookFunctional.AddOrUpdateContact(x, homer))
//                .Bind(x => AddressBookFunctional.SendConfirmationMail2(mailer, homer, x));
//        }

        private class Mailer : IMailer
        {
            public Either<string, Contact> Send(Contact contact)
            {
                return Right(contact);
            }
        }
    }
}