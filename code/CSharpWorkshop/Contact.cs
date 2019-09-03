using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using String = System.String;

namespace CSharpDemos
{
    public class Contact
    {
        public string FirstName { get; }
        public string LastName { get; }
        public Option<DateTime> DateOfBirth { get; }
        public string TwitterHandle { get; }

        private Contact(
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
            this.DateOfBirth = dateOfBirth.Map(StripTime);
            this.TwitterHandle = twitterHandle;
        }

        public static Func<string, string, Option<DateTime>, string, Contact> 
            Create = (
            firstName,
            lastName,
            dateOfBirth,
            twitterHandle) 
                => new Contact(firstName, lastName, dateOfBirth, twitterHandle);

        public static Validation<Contact> CreateValidContact(
            string fn, string ln, Option<DateTime> dob, string twitter)
            => Valid(Create)
                .Apply(FirstNameNotEmpty(fn))
                .Apply(LastNameNotEmpty(ln))
                .Apply(dob)
                .Apply(twitter);

        public static Validation<string> FirstNameNotEmpty(string name)
            => String.IsNullOrWhiteSpace(name)
                ? Error("First name is empty")
                : Valid(name);
        
        public static Validation<string> LastNameNotEmpty(string name)
            => String.IsNullOrWhiteSpace(name)
                ? Error("Last name is empty")
                : Valid(name);

        public string Stringify()
        {
            string output = LastName + ", " + FirstName;
            return DateOfBirth.Match(
                () => output,
                x => output + ", " + x.ToString("yyyy-MM-dd")
            );
        }

        private DateTime StripTime(DateTime dateTime) => dateTime.Date;

    }

    public static class ContactExtension
    {
        private static Either<string, Contact> Save(this Contact contact) => Left("Dienstag");
        private static Either<string, Contact> SendEmail(this Contact contact) => Left("Mittwoch");

        public static Either<string, Contact> SaveAndSendEmail(this Contact contact)
            => Save(contact)
                .Bind(SendEmail);

        public static string Output(this Either<string, Contact> contact)
        {
            return contact.Match(
                s => s,
                c => c.Stringify());
        }

        public static string SaveAndSendEmailAndOutput(this Contact contact)
        {
            return contact.SaveAndSendEmail().Output();
        }
    }
}
