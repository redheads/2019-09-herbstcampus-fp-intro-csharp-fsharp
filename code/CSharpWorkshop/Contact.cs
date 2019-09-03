using System;
using LaYumba.Functional;

namespace CSharpDemos
{
    public class Contact
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Option<DateTime> DateOfBirth { get; }
        public string TwitterHandle { get; }

        public Contact(
            string firstName,
            string lastName,
            Option<DateTime> dateOfBirth,
            string twitterHandle)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                throw new Exception();
            }

            this.LastName = lastName;
            this.FirstName = firstName;
            this.DateOfBirth = dateOfBirth.Map(StripTime).Map(StripTime2);
            this.TwitterHandle = twitterHandle;
        }

        public string Stringify()
        {
            string output = LastName + ", " + FirstName;
            return DateOfBirth.Match(
                () => output,
                x => output + ", " + x.ToString("yyyy-MM-dd")
            );
        }

        private DateTime StripTime(DateTime dateTime) => dateTime.Date;
        private DateTime StripTime2(DateTime dateTime) => dateTime.AddDays(1);
    }
}
