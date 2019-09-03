﻿using System;

namespace CSharpDemos
{
    public class Contact
    {
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime? DateOfBirth { get; }
        public string TwitterHandle { get; }

        public Contact(
            string firstName,
            string lastName,
            DateTime? dateOfBirth,
            string twitterHandle)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new Exception();
            }
            this.LastName = lastName;
            this.FirstName = firstName;
            this.DateOfBirth = dateOfBirth;
            this.TwitterHandle = twitterHandle;
        }
        public string Stringify()
        {
            string output = LastName + ", " + FirstName;
            if (DateOfBirth.HasValue)
                output += ", " + DateOfBirth;

            return output;
        }
    }
}
