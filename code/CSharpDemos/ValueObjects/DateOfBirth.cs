using System;
using System.Collections.Generic;

namespace CSharpDemos.ValueObjects
{
    public class DateOfBirth : ValueObject
    {
        public DateOfBirth(DateTime value)
        {
            Value = value.Date;
        }

        public DateTime Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator DateTime(DateOfBirth dob)
        {
            return dob.Value;
        }

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd");
        }
    }
}