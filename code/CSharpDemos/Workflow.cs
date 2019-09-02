using System;
using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace CSharpDemos
{
    public static class Workflow
    {
        
        // DON'T INJECT AN IMAILER!! No IoC!! Use a function!!
        //
//        public static Func<IMailer, Contact, AddressBook, Either<string, AddressBook>> SendConfirmationMail
//            = (mailer, contact, addressBook)
//                => mailer.Send(contact)
//                    .Match<Either<string, AddressBook>>(
//                        errorMsg => Left(errorMsg),
//                        _ => Right(addressBook));

        public static Either<string, AddressBook> SendConfirmationMailOpt(this AddressBook addressBook,
            Func<Contact, Either<string, Contact>> sendMailTo, Contact contact)
            => sendMailTo(contact)
                .Match<Either<string, AddressBook>>(
                    errorMsg => Left(errorMsg),
                    _ => Right(addressBook));

        public static Either<string, AddressBook> AddWorkflow(this AddressBook addressBook,
            Func<Contact, Either<string, Contact>> sendMailFunction, Contact contact)
            => Right(addressBook)
                .Bind(x => x.AddOrUpdateContactOpt(contact))
                .Bind(x => x.SendConfirmationMailOpt(sendMailFunction, contact));
    }
    
    // Don't use this!
//    public interface IMailer
//    {
//        Either<string, Contact> Send(Contact contact);
//    }
}