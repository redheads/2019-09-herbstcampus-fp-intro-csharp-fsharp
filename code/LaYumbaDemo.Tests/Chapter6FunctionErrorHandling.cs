using System;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;

using static LaYumba.Functional.F; // <- !!

namespace LaYumbaDemo.Tests
{
    // ========================================================================================
    // Chapter 6: Functional error handling
    // - Either
    public class Chapter6FunctionErrorHandling
    {
        [Fact]
        public void ScratchEither()
        {
            // Either
            var r = Right(12);
            var l = Left("ups");
            true.Should().BeTrue();
        }

        private string Render(Either<string, double> val)
        {
            return val.Match(
                l => $"Invalid value: {l}",
                r => $"The result is: {r}");
        }

        // Listing 6.1
        // f(x, y) -> sqrt(x / y)
        private Either<string, double> Calc(double x, double y)
        {
            if (y == 0) return "y cannot be 0";
            if (x != 0 && Math.Sign(x) != Math.Sign(y))
                return "x / y cannot be negative";
            return Math.Sqrt(x / y);
        }

        // Listing 6.2 (using Option)
        private Option<Candidate> RecruitmentProcess1(Candidate candidate,
            Func<Candidate, bool> isEligible,
            Func<Candidate, Option<Candidate>> techTest,
            Func<Candidate, Option<Candidate>> interview)
        {
            return F.Some(candidate)
                .Where(isEligible)
                .Bind(techTest) // <- TODO Explain Bind
                .Bind(interview);
        }

        // Listing 6.3 (using Either instead of Option)
        private Either<Rejection, Candidate> RecruitmentProcess2(
            Candidate candidate,
            Func<Candidate, Either<Rejection, Candidate>> checkEligibility,
            Func<Candidate, Either<Rejection, Candidate>> techTest,
            Func<Candidate, Either<Rejection, Candidate>> interview)
        {
            return F.Right(candidate)
                .Bind(checkEligibility) // <- TODO explain Bind
                .Bind(techTest) // <- TODO explain Bind
                .Bind(interview);
        }

        // Listing 6.4
        //private void Chaining_Either()
        //{
        //    Func<Either<Reason, Unit>> WakeUpEarly;
        //    Func<Unit, Either<Reason, Ingredients>> ShopForIngredients;
        //    Func<Ingredients, Either<Reason, Food>> CookRecipe;
        //    Action<Food> EnjoyTogether;
        //    Action<Reason> ComplainAbout;
        //    Action OrderPizza;

        //    /*
        //         o WakeUpEarly
        //        / \
        //       L   R ShopForIngredients
        //          / \
        //         L   R CookRecipe
        //            / \
        //           L   R EnjoyTogether
        //    */

        //    void Start()
        //    {
        //        WakeUpEarly()
        //            .Bind(ShopForIngredients)
        //            .Bind(CookRecipe)
        //            .Match(
        //                Right: EnjoyTogether,
        //                Left: reason =>
        //                {
        //                    ComplainAbout(reason);
        //                    OrderPizza();
        //                });
        //    }
        //}

        private class Candidate
        {
            public Candidate(string name)
            {
                Name = name;
            }

            public string Name { get; }
        }

        private class Rejection
        {
            public Rejection(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; }
        }

        private class Reason
        {
        }

        private class Ingredients
        {
        }

        private class Food
        {
        }

        [Fact]
        public void Age_has_smart_ctor()
        {
            // TODO Create readable test extension for Option
            Age.Of(10).Match(
                () => true.Should().Be(false),
                age => age.Value.Should().Be(10));

            Age.Of(-1).Match(
                () => true.Should().Be(true),
                age => age.Should().Be(null));
        }

        [Fact]
        public void CalcTest()
        {
            // TODO Is there a easier way to test an Either??
            Calc(3, 0).Match(
                e => e.Should().Be("y cannot be 0"),
                r => r.Should().Be(null));

            Calc(-3, 3).Match(
                e => e.Should().Be("x / y cannot be negative"),
                r => r.Should().Be(null));

            Calc(-3, -3).Match(
                e => e.Should().Be(null),
                r => r.Should().Be(1));
        }

        [Fact]
        public void RecruitmentProcess1Test()
        {
            // Arrange
            Func<Candidate, bool> isEligible = candidate => true;
            Func<Candidate, Option<Candidate>> techTest = candidate => F.Some(candidate);
            Func<Candidate, Option<Candidate>> interview = candidate => F.Some(candidate);

            // Act
            var optionalCandidate = RecruitmentProcess1(new Candidate("homer"), isEligible, techTest, interview);

            // Assert
            optionalCandidate.Map(c => c.Name.Should().Be("homer"));
        }

        [Fact]
        public void RecruitmentProcess2Test()
        {
            // Arrange
            Func<Candidate, bool> isEligible = candidate => true;
            Func<Candidate, Either<Rejection, Candidate>> techTest = candidate => F.Right(candidate);
            Func<Candidate, Either<Rejection, Candidate>> interview = candidate => F.Right(candidate);

            Either<Rejection, Candidate> CheckEligibility(Candidate c)
            {
                if (isEligible(c)) return c;
                return new Rejection("Not eligible");
            }

            // Act
            var optionalCandidate = RecruitmentProcess2(
                new Candidate("homer simpson"),
                CheckEligibility, techTest, interview);

            // Assert
            optionalCandidate.Map(candidate => candidate.Name.Should().Be("homer simpson"));
        }

        [Fact]
        public void RenderTest()
        {
            Render(F.Right(12d)).Should().Be("The result is: 12");
            Render(F.Left("ups")).Should().Be("Invalid value: ups");
        }

        [Fact]
        public void Using_pattern_matching()
        {
            string GreetClassic(string greetee)
            {
                return greetee == null
                    ? "Sorry, who?"
                    : $"Hello, {greetee}";
            }

            GreetClassic(null).Should().Be("Sorry, who?");
            GreetClassic("dodnedder").Should().Be("Hello, dodnedder");


            string greet(Option<string> greetee)
            {
                return greetee.Match(
                    () => "Sorry, who?",
                    name => $"Hello, {name}");
            }

            // string greetCompact(Option<string> greetee)
            //     => greetee.Match(
            //         () => "Sorry, who?",
            //         (name) => $"Hello, {name}");

            Option<string> none = F.None;
            var dodnedder = F.Some("dodnedder");

            greet(none).Should().Be("Sorry, who?");
            greet(dodnedder).Should().Be("Hello, dodnedder");
        }


    }
}