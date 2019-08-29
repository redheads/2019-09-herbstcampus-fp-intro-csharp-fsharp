using System;
using FluentAssertions;
using Xunit;

namespace CSharpDemos.Tests
{
    public class DummyTests
    {
        [Fact]
        public void Dummy()
        {
            var sut = new Class1();
            sut.AddOne(1).Should().Be(2);
        }
    }
}
