using System;
using System.Collections.Generic;
using LaYumba.Functional;

namespace CSharpDemos.ValueObjects
{
    public class Id : ValueObject
    {
        // smart ctor
        public static Func<int, Option<Id>> Create
            = i => i.IsValidId()
                ? F.Some(new Id(i))
                : F.None;

        private Id(int value)
        {
            Value = value;
        }

        public int Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator int(Id id)
        {
            return id.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}