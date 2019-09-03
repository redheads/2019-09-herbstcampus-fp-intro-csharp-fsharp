using System;

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
            this.LastName = lastName;
            this.FirstName = firstName;
            this.DateOfBirth = dateOfBirth;
            this.TwitterHandle = twitterHandle;
        }
        public string Stringify()
        {
            return this.LastName + ", " + this.FirstName;
        }
    }
}
