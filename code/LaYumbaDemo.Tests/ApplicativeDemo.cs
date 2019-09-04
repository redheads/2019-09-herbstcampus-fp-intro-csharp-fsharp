using System;
using System.Linq;
using FluentAssertions;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using Xunit;

namespace LaYumbaDemo.Tests
{
    public class ApplicativeDemo
    {
        [Fact]
        public void Sum_validation()
        {
            // Arrange
            Func<int, int, int, int> sum = (a, b, c) => a + b + c;

            Func<int, Validation<int>> onlyPositive = i 
                => i > 0 
                    ? Valid(i) 
                    : Error($"Number {i} is not positive.");
            
            Validation<int> AddNumbers(int a, int b, int c) {
                return Valid(sum)              // returns int -> int -> int -> int
                    .Apply(onlyPositive(a))    // returns int -> int -> int
                    .Apply(onlyPositive(b))    // returns int -> int
                    .Apply(onlyPositive(c));   // returns int
            }

            // Act
            var result = AddNumbers(1, 2, 3);
            
            // Assert
            result.Match(
                _ => true.Should().BeFalse(),
                x => x.Should().Be(6));
        }

        [Fact]
        public void Sum_validation_with_failures()
        {
            // Arrange
            Func<int, int, int, int> sum = (a, b, c) => a + b + c;

            Func<int, Validation<int>> onlyPositive = i 
                => i > 0 
                    ? Valid(i) 
                    : Error($"Number {i} is not positive.");
            
            Validation<int> AddNumbers(int a, int b, int c) {
                return Valid(sum)              // returns int -> int -> int -> int
                    .Apply(onlyPositive(a))    // returns int -> int -> int
                    .Apply(onlyPositive(b))    // returns int -> int
                    .Apply(onlyPositive(c));   // returns int
            }

            // Act
            var result = AddNumbers(-1, -2, -3);
            
            // Assert
            result.Match(
                errors => errors.Select(x => x.Message)
                    .Should().Contain("Number -1 is not positive.")
                    .And.Contain("Number -2 is not positive.")
                    .And.Contain("Number -3 is not positive."),
                x => true.Should().BeFalse());
        }
    }
}