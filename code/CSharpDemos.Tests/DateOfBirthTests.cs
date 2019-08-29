using System;
using FluentAssertions;
using Xunit;

namespace CSharpDemos.Tests
{
    public class DateOfBirthTests
    {
        [Fact]
        public void Formatting()
        {
            new DateOfBirth(new DateTime(1900, 12, 31, 23, 59, 59)).ToString()
                .Should().Be("1900-12-31");
        }
    }
}