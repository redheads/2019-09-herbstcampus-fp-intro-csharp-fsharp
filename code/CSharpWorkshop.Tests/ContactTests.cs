using System;
using CSharpDemos;
using FluentAssertions;
using Xunit;

namespace CSharpWorkshop.Tests
{
    public class ContactTests
    {
        [Theory]
        [InlineData("Homer", "Simpson", true)]
        [InlineData("", "Simpson", false)]
        [InlineData("Homer", "", false)]
        public void ConstructorValidation_works(string firstName, string lastName, bool isValid)
        {
            Action action = () => new Contact(firstName, lastName, null, null);
            if (isValid)
            {
                action.Should().NotThrow();
            }
            else
            {
                action.Should().Throw<Exception>();
            }
        }
    }
}