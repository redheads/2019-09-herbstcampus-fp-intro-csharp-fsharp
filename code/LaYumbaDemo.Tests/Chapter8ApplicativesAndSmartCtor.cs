using System;
using System.Collections.Generic;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;
using static LaYumba.Functional.F;
using Enum = LaYumba.Functional.Enum;

namespace LaYumbaDemo.Tests
{
    // Keywords: applicative, validation, smart ctor
    public class Chapter8ApplicativesAndSmartCtor
    {
    }

    public class PhoneNumberValidationTests
    {
        ISet<string> ValidCountryCodes = new HashSet<string> { "ch", "uk", "de" };

        // string -> Validation<CountryCode>
        Func<string, Validation<CountryCode>> validCountryCode
           => s => CountryCode.Create(ValidCountryCodes, s).Match(
              None: () => Error($"{s} is not a valid country code"),
              Some: c => Valid(c));

        // string -> Validation<PhoneNumber.NumberType>
        Func<string, Validation<PhoneNumber.NumberType>> validNumberType
           = s => Enum.Parse<PhoneNumber.NumberType>(s).Match(
              None: () => Error($"{s} is not a valid number type"),
              Some: n => Valid(n));

        // string -> Validation<PhoneNumber.Number>
        Func<string, Validation<Number>> validNumber
           = s => Number.Create(s).Match(
              None: () => Error($"{s} is not a valid number"),
              Some: n => Valid(n));

        Validation<PhoneNumber> CreateValidPhoneNumber(string type, string countryCode, string number)
            => Valid(PhoneNumber.Create)
                .Apply(validNumberType(type))
                .Apply(validCountryCode(countryCode))
                .Apply(validNumber(number));

        //Validation<PhoneNumber> CreateValidPhoneNumber2(string type, string countryCode, string number)
        //{
        //    Validation<Func<PhoneNumber.NumberType, CountryCode, Number, PhoneNumber>> validation = Valid(PhoneNumber.Create);

        //    Validation<PhoneNumber.NumberType> numberType = validNumberType(type);
        //    return validation.Ap
        //        .Apply(numberType)
        //        .Apply(validCountryCode(countryCode))
        //        .Apply(validNumber(number));
        //}

        [Theory]
        [InlineData("Mobile", "ch", "123456", "Valid(Mobile: (ch) 123456)")]
        [InlineData("Mobile", "xx", "123456", "Invalid([xx is not a valid country code])")]
        [InlineData("Mobile", "xx", "1", "Invalid([xx is not a valid country code, 1 is not a valid number])")]
        [InlineData("rubbish", "xx", "1", "Invalid([rubbish is not a valid number type, xx is not a valid country code, 1 is not a valid number])")]
        public void Collecting_errors_using_applicative_validation(string type, string country, string number, string expected)
            => CreateValidPhoneNumber(type, country, number).ToString().Should().Be(expected);
    }

    public class PhoneNumber
    {
        public enum NumberType { Mobile, Home, Office }

        public NumberType Type { get; }
        public CountryCode Country { get; }
        public Number Nr { get; }

        // smart ctor: Returns a function (!!) which returns a PhoneNumber
        public static Func<NumberType, CountryCode, Number, PhoneNumber> Create 
            = (type, country, number) => new PhoneNumber(type, country, number);

        private PhoneNumber(NumberType type, CountryCode country, Number number)
        {
            Type = type;
            Country = country;
            Nr = number;
        }

        public override string ToString() => $"{Type}: ({Country}) {Nr}";
    }

    public class CountryCode
    {
        // smart ctor
        // ISet<string> -> string -> Option<CountryCode>
        public static Func<ISet<string>, string, Option<CountryCode>>
        Create = (validCodes, code)
           => validCodes.Contains(code)
              ? Some(new CountryCode(code))
              : None;

        public string Value { get; }

        // private ctor so that no invalid instances may be created
        private CountryCode(string value) { Value = value; }

        public override string ToString() => Value;
    }

    public class Number
    {
        // smart ctor
        public static Func<string, Option<Number>> Create
            = s => Long.Parse(s)
               .Map(_ => s) // <- "map": extract value from Option..
               .Where(_ => 5 < s.Length && s.Length < 11) // <- "filter" valid length entries
               .Map(_ => new Number(s)); // "reduce"

        string Value { get; }

        private Number(string value) { Value = value; }

        public static implicit operator string(Number c) => c.Value;
        public static implicit operator Number(string s) => new Number(s);

        public override string ToString() => Value;
    }
}