using System;
using System.Collections.Generic;
using System.Globalization;

namespace CSharpDemos
{
    public class DateOfBirth : ValueObject
    {
        public DateTime Value { get; }

        public DateOfBirth(DateTime value) => Value = value.Date;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public static implicit operator DateTime(DateOfBirth dob) => dob.Value;

        public override string ToString() => Value.ToString("yyyy-MM-dd");
    }
}