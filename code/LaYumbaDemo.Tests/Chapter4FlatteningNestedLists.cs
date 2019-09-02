using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using Xunit;

namespace LaYumbaDemo.Tests
{
    // FP-JARGON: Filter, Map; page 94
    /*
        LaYumba.Functional              LINQ                Common synonyms
        ------------------              ----                ---------------
        Map                             Select              fMap, Project, Lift
        Bind                            SelectMany          FlatMap, Chain, Collect, Then
        Where                           Where               Filter
        ForEach                         n/a                 Iter
     */

    // FP-JARGON: Fig. 4.4, Fig. 4.5, Fig. 4.6

    public class Chapter4FlatteningNestedLists
    {
        // 4.3.2 Flattening nested lists with Bind

        /*
            public static IEnumerable<R> Bind<T, R>
                (this IEnumerable<T> ts, Func<T, IEnumerable<R>> f)
            {
                foreach (T t in ts)
                    foreach (R r in f(t))
                        yield return r;
            }
         */

        // Previous example is the same as Linq SelectMany!

        [Fact]
        public void Flattening_nested_list_works()
        {
            // Arrange
            var neighbors = new List<Neighbor>
            {
                new Neighbor {FirstName = "John", Pets = new List<string> {"Fluffy", "Thor"}},
                new Neighbor {FirstName = "Tim"},
                new Neighbor {FirstName = "Carl", Pets = new List<string> {"Sybil"}}
            };

            // Act
            var pets = neighbors.Bind(n => n.Pets);
            //var pets = neighbors.SelectMany(n => n.Pets);
            
            // Assert
            pets.Should().BeEquivalentTo(new List<string> {"Fluffy", "Thor", "Sybil"});
        }

        private class Neighbor
        {
            public string FirstName { get; set; }
            public List<string> Pets { get; set; } = new List<string>();
        }

                // ========================================================================================
        // Chapter 4: Map, Bind, Where and ForEach; Functors and Monads

        // 4.1 Applying a function to a structure's inner value

        // Listing 4.1 Map for IEnumerable<T>
        /*
            public static IEnumerable<R> Map<T, R>
                (this IEnumerable<T> ts, Func<T, R> f)
            {
                foreach (var t in ts)
                    yield return f(t);
            }
         */

        // FP-JARGON: Map == Linq's Select

        // Map Signature for IEnumerable:   (IEnumerable<T>, (T -> R)) -> IEnumerable<R>

        // Porting the principle to Option: (Option<T>,      (T -> R)) -> Option<R>

        /*
            // Handle None case
            public static Option<R> Map<T, R>
                (this Option.None _, Func<T, R> f)
                    => None;

            // Handle Some case
            public static Option<R> Map<T, R>
                (this Option.Some<T> some, Func<T, R> f)
                    => Some(f(some.Value));                    

            // Combined
            public static Option<R> Map<T, R>
                (this Option<T> optT, Func<T, R> f)
                    => optT.Match(
                        () => None,
                        (t) => Some(f(t)));                    
         */

        // Abstract pattern: Map ("C" short for "Container"): 
        //          (C<T>, (T -> R)) -> C<R>
        //
        // FP-JARGON: This is called a Functor
        //
        // Why is a functor not an interface? C# does not support HKTs! See Box on page 86

        // ForEach: Used for performing side-effects! 
        // This similar to the difference between Action and Func:
        //  Action has no return value -> must be performing a side-effect)

        // 4.3 Bind: Chaining functions which return a Container
        //
        //      Abstract pattern: Bind ("C" short for "Container"):
        //          (C<T>, (T -> C<R>)) -> C<R>
        //
        // FP-JARGON: This is called a Monad
        //
        // FP-JARGON: Bind == Linq's SelectMany
        //
        // Listing 4.3 Comparing Map and Bind
        /*
            public static Option<R> Bind<T, R>
                (this Option<T> optT, Func<T, Option<R>> f) // <- Bind takes an Option-returning function!
                    => optT.Match(
                        () => None,
                        (t) => f(t));

            public static Option<R> Map<T, R>
                (this Option<T> optT, Func<T, R> f) // <- Map takes a regular function!
                    => optT.Match(
                        () => None,
                        (t) => Some(f(t)));     
         */

        // Listing 4.4 Using Bind to compose two functions that return an Option
        [Fact]
        public void BindDemo()
        {
            // Int.Parse is a function from LaYumba. It takes a string and returns an Option<int>.
            // Int.Parse: s -> Option<int>
            var optI = Int.Parse("12");

            // Age.Of: int -> Option<Age>
            // Age.Of(1)

            // Combination with Map:
            var ageOpt = optI.Map(i => Age.Of(i));

            // Problem: returns Option<Option<Age>> ARRGH!
            var ageOpt1 = optI.Map(i => Age.Of(i));

            // Solution: Bind instead of Map when combining functions which return M<T>
            Func<string, Option<Age>> parseAge = s
                => Int.Parse(s).Bind(Age.Of);

            var ageO = parseAge("12");
            ageO.Match(
                () => true.Should().Be(false), // <- ensure that this path is never called!
                x => x.Value.Should().Be(12)
            );
        }


    }
}