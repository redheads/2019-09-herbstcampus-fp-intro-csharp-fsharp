using CSharpDemos.ValueObjects;
using FluentAssertions;
using Xunit;
using static LaYumba.Functional.F;

namespace CSharpDemos.Tests
{
    public class ContactTests
    {
        [Theory]
        [InlineData(1, "Homer", "Simpson", "Valid(Id: 1: Homer Simpson (DOB: None, Twitter: None))", true)]
        [InlineData(0, "Homer", "Simpson", "Invalid([invalid Id])", false)]
        [InlineData(1, "", "Simpson", "Invalid([invalid FirstName])", false)]
        [InlineData(1, "Homer", "", "Invalid([invalid LastName])", false)]
        [InlineData(0, "", "", "Invalid([invalid Id, invalid FirstName, invalid LastName])", false)]
        public void Contact_creation(int id, string firstName, string lastName, string expectedMessage, bool isValid)
        {
            // Arrange
            var optId = Id.Create(id);
            var optFirstName = NonEmptyString.Create(firstName);
            var optLastName = NonEmptyString.Create(lastName);
            var optDob = None;
            var optTwitterHandle = None;
            
            // Act
            var validContact = Contact.CreateValidContact(optId, optFirstName, optLastName, optDob, optTwitterHandle);

            // Assert
            validContact.IsValid.Should().Be(isValid);
            validContact.ToString().Should().Be(expectedMessage);
        }
    }
}