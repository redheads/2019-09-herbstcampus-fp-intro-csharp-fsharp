using System;
using System.Linq;
using CSharpDemos;
using FluentAssertions;
using static LaYumba.Functional.F;
using Xunit;

namespace CSharpWorkshop.Tests
{
    public class ContactTests
    {
//        [Theory]
//        [InlineData("Homer", "Simpson", true)]
//        [InlineData("", "Simpson", false)]
//        [InlineData("Homer", "", false)]
//        public void ConstructorValidation_works(string firstName, string lastName, bool isValid)
//        {
//            Action action = () => new Contact(firstName, lastName, None, null);
//            if (isValid)
//            {
//                action.Should().NotThrow();
//            }
//            else
//            {
//                action.Should().Throw<Exception>();
//            }
//        }

//        [Theory]
//        [InlineData("Homer", "Simpson", null, "Simpson, Homer")]
//        [InlineData("Homer", "Simpson", "2019.1.1", "Simpson, Homer, 2019-01-01")]
//        public void Stringify_works(string firstName, string lastName, string dateOfBirth, string result)
//        {
//            var d = dateOfBirth == null ? None : Some(DateTime.Parse(dateOfBirth));
//            var c = new Contact(firstName, lastName, d, null);
//            c.Stringify().Should().Be(result);
//        }

        [Fact]
        public void ContactCreationCollectsErrorMessages()
        {
            var result = Contact.CreateValidContact("", "", None, null);
            result.Match(
                error => error.Select(x => x.Message)
                    .Should().Contain("First name is empty")
                    .And.Contain("Last name is empty"),
                contact => true.Should().BeFalse());
        }
    }
}