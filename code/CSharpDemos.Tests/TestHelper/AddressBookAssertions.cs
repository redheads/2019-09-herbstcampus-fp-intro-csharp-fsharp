using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using LaYumba.Functional;

namespace CSharpDemos.Tests.TestHelper
{
    public static class AddressBookTestExtensions 
    {
        public static AddressBookAssertions Should(this Either<string, AddressBook> instance)
        {
            return new AddressBookAssertions(instance);
        }
    }

    public class AddressBookAssertions
        : ReferenceTypeAssertions<Either<string, AddressBook>, AddressBookAssertions>
    {
        public AddressBookAssertions(Either<string, AddressBook> instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "AddressBook";

        public AndConstraint<AddressBookAssertions> BeEqualToAddressBook(
            string otherString,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!string.IsNullOrWhiteSpace(otherString))
                .FailWith("You can't compare AddressBook if you provide an empty AddressBook.")
                .Then
                .Given(() => Subject)
                .ForCondition(opt => opt.Match(
                    _ => false,
                    x => x.ToString() == otherString))
                .FailWith("Expected {context:AddressBook} to be {0}{reason}, but found {1}",
                    otherString, Subject);

            return new AndConstraint<AddressBookAssertions>(this);
        }

        public AndConstraint<AddressBookAssertions> NotBeEqualAddressBook(
            string otherString,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .Given(() => Subject)
                .ForCondition(opt => opt.Match(
                    _ => false,
                    x => x.ToString() != otherString))
                .FailWith("Expected {context:AddressBook} not to be {0}{reason}, but found {1}",
                    otherString, Subject);

            return new AndConstraint<AddressBookAssertions>(this);
        }

        public AndConstraint<AddressBookAssertions> HaveErrorMessage(
            string otherString,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .Given(() => Subject)
                .ForCondition(opt => opt.Match(
                    errMsg => errMsg == otherString,
                    x => false))
                .FailWith("Expected {context:AddressBook} to have error {0}{reason}, but found {1}",
                    otherString, Subject);

            return new AndConstraint<AddressBookAssertions>(this);
        }

        public AndConstraint<AddressBookAssertions> HaveNoErrors(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .Given(() => Subject)
                .ForCondition(opt => opt.Match(
                    _ => false,
                    _ => true))
                .FailWith("Expected {context:AddressBook} to have no errors, but found {1}",
                    Subject);

            return new AndConstraint<AddressBookAssertions>(this);
        }

//        public AndConstraint<AddressBookAssertions> BeNone(
//            string because = "",
//            params object[] becauseArgs)
//        {
//            Execute.Assertion
//                .Given(() => Subject)
//                .ForCondition(opt => opt.Match(
//                    () => true,
//                    x => false))
//                .FailWith("Expected {context:AddressBook} to be None {reason}, but found {0}",
//                    Subject);
//
//            return new AndConstraint<AddressBookAssertions>(this);
//        }
    }
}