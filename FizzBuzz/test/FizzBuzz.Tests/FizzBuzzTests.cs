using FluentAssertions;

namespace FizzBuzz.Tests;

public class FizzBuzzTests {

    [Test]
    public void print_a_number() {
        var fizzBuzz = new FizzBuzz();

        var output = fizzBuzz.Execute(1);

        output.Should().Be("1");
    }

    [Test]
    public void print_fizz_when_number_is_multiple_of_three() {
        var fizzBuzz = new FizzBuzz();

        var output = fizzBuzz.Execute(3);

        output.Should().Be("Fizz");
    }

    [Test]
    public void print_buzz_when_number_is_multiple_of_five() {
        var fizzBuzz = new FizzBuzz();

        var output = fizzBuzz.Execute(5);

        output.Should().Be("Buzz");
    }

    [Test]
    public void print_fizzbuzz_when_number_is_multiple_of_three_and_five() {
        var fizzBuzz = new FizzBuzz();

        var output = fizzBuzz.Execute(15);

        output.Should().Be("FizzBuzz");
    }
}