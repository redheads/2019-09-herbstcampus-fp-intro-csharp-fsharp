using System.Collections.Immutable;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;
using static LaYumba.Functional.F;
using static CSharpDemos.Tests.TestHelper.ContactHelper;

namespace CSharpDemos.Tests
{
    public class AddressBookTests
    {
        [Fact]
        public void Adding_contacts_increments_entries_in_address_book()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var marge = CreateSampleContact(2, "Marge", "Simpson");
            
            // Act
            var addressBookOpt = Right(emptyAddressBook)
                .Bind(x => x.AddOrUpdateContactOpt(homer))
                .Bind(x => x.AddOrUpdateContactOpt(marge));
            
            // Assert
//            addressBookOpt.Match(
//                (e) => e.Should().Be("ups"),
//                (x) => x.Contacts.Should().HaveCount(emptyAddressBook.Contacts.Count + 2));
            
            addressBookOpt.ToString().Should().Be("Right(AddressBook. Number of entries: 2)");
        }

        [Fact]
        public void Updating_contact_in_address_book()
        {
            // Arrange
            var emptyAddressBook = new AddressBook(ImmutableList<Contact>.Empty);
            
            var homer = CreateSampleContact(1, "Homer", "Simpson");
            var homerWithTwitter = CreateSampleContact(1, "Homer", "Simpson", null, "@homer");
            
            // Act
            var addressBookOpt = Right(emptyAddressBook)
                .Bind(x => x.AddOrUpdateContactOpt(homer))
                .Bind(x => x.AddOrUpdateContactOpt(homerWithTwitter));
            
            // Assert
            addressBookOpt.ToString().Should().Be("Right(AddressBook. Number of entries: 1)");
            var contact = addressBookOpt.UnsafeUnpack().Contacts.First();
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
            
            // Act
            var addressBookOpt = Right(emptyAddressBook)
                .Bind(x => x.AddOrUpdateContactOpt(homer))
                .Bind(x => x.AddOrUpdateContactOpt(marge))
                .Bind(x => x.RemoveOpt(homer));
            
            // Assert
            addressBookOpt.ToString().Should().Be("Right(AddressBook. Number of entries: 1)");
            var contact = addressBookOpt.UnsafeUnpack().Contacts.First();
            contact.Id.Value.Should().Be(2);
            contact.FirstName.ToString().Should().Be("Marge");
        }
    }
}