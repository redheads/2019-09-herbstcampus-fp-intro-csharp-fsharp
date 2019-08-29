using System;
using System.Collections.Generic;
using LaYumba.Functional;

namespace CSharpDemos
{
    public class NonEmptyString : ValueObject
    {
        public string Value { get; }
        private NonEmptyString(string potentialString) => Value = potentialString;

        // smart ctor
        public static Func<string, Option<NonEmptyString>> Create 
            = s => s.IsNonEmpty()
                ? F.Some(new NonEmptyString(s))
                : F.None;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(NonEmptyString nonEmptyString) 
            => nonEmptyString.Value;

        public override string ToString() => Value;
    }
}