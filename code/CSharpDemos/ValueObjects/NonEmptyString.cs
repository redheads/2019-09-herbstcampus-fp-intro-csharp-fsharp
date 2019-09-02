using System;
using System.Collections.Generic;
using LaYumba.Functional;

namespace CSharpDemos.ValueObjects
{
    public class NonEmptyString : ValueObject
    {
        // smart ctor
        public static Func<string, Option<NonEmptyString>> Create
            = s => s.IsNonEmpty()
                ? F.Some(new NonEmptyString(s))
                : F.None;

        private NonEmptyString(string potentialString)
        {
            Value = potentialString;
        }

        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(NonEmptyString nonEmptyString)
        {
            return nonEmptyString.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}