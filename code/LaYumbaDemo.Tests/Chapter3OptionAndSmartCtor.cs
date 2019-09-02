using System;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;
using Unit = System.ValueTuple; // <- don't use void!!

using static LaYumba.Functional.F; // <- !!

namespace LaYumbaDemo.Tests
{
    // ========================================================================================
    // Chapter 3: Option (requires pattern matching)
    public class Chapter3OptionAndSmartCtor
    {
        [Fact]
        public void Creating_option_with_value_works()
        {
            var s = Some("hello");
            
            s.Match(
                None: () => true.Should().BeFalse(),
                Some: x => x.Should().Be("hello"));
        }

        [Fact]
        public void Creating_empty_option_works()
        {
            Option<string> s = None;
            s.Match(
                None: () => true.Should().BeTrue(),
                Some: x => x.Should().BeEmpty());
        }

        [Fact]
        public void Using_a_smart_ctor_works()
        {
            var optAge = Age.Of(10);
            optAge.Should().BeOfType<Option<Age>>();
        }
    }

    // Listing 3.8 Smart constructor pattern
    public struct Age
    {
        public int Value { get; }

        // smart ctor
        public static Option<Age> Of(int age)
        {
            return IsValid(age) ? Some(new Age(age)) : F.None;
        }

        private Age(int value)
        {
            if (!IsValid(value))
                throw new ArgumentException($"{value} is not a valid age");
            Value = value;
        }

        private static bool IsValid(int age)
        {
            return 0 <= age && age < 120;
        }
    }
}