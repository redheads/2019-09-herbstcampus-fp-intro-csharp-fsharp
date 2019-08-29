using System;
using System.Collections.Generic;
using LaYumba.Functional;

namespace CSharpDemos
{
    public class Id : ValueObject
    {
        public int Value { get; }

        private Id(int value) => Value = value;

        // smart ctor
        public static Func<int, Option<Id>> Create 
            = i => i.IsValidId()
                ? F.Some(new Id(i))
                : F.None;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator int(Id id) => id.Value;
        
        public override string ToString() => Value.ToString();
    }
}