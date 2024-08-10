using FluentAssertions;

namespace StringCalculator.Tests;

public class StringCalculatorTests {


    [TestCase("", 0)]
    [TestCase("1", 1)]
    [TestCase("1,2", 3)]
    [TestCase("1.2,2.3", 3.5f)]
    [TestCase("1.1,1.1,1.1,1.1,1.1", 5.5f)]
    [TestCase("1\n2,3", 6)]
    public void sum_numbers(string value, double expectedResult) {
        var stringCalculator = new StringCalculator();

        var result = stringCalculator.Add(value);

        result.Should().Be(expectedResult);
    }

    [Test]
    public void invalid_sum_numbers() {
        var stringCalculator = new StringCalculator();

        Action action = () => stringCalculator.Add("175.2,\n35");

        action.Should().Throw<Exception>()
            .And.Message.Should().Be("Number expected but '\n' found at position 6.");
    }

    [Test]
    public void missing_number_in_last_position() {
        var stringCalculator = new StringCalculator();

        Action action = () => stringCalculator.Add("1,3,");

        action.Should().Throw<Exception>()
            .And.Message.Should().Be("Number expected but EOF found.");
    }


    [TestCase("//;\n1;2", 3)]
    [TestCase("//|\n1|2|3", 6)]
    [TestCase("//sep\n2sep3", 5)]
    public void allow_custom_separators(string value, double expectedResult) {
        var stringCalculator = new StringCalculator();

        var result = stringCalculator.Add(value);

        result.Should().Be(expectedResult);
    }

    [Test]
    public void bad_custom_separator_ussage() {
        var stringCalculator = new StringCalculator();

        Action action = () => stringCalculator.Add("//|\n1|2,3");

        action.Should().Throw<Exception>()
            .And.Message.Should().Be("'|' expected but ',' found at position 3.");
    }


    [Test]
    public void do_not_allow_negative_numbers() {
        var stringCalculator = new StringCalculator();

        Action action = () => stringCalculator.Add("-1,2");

        action.Should().Throw<Exception>()
            .And.Message.Should().Be("Negative not allowed : -1");
    }

    [Test]
    public void do_not_allow_multiple_negative_numbers() {
        var stringCalculator = new StringCalculator();

        Action action = () => stringCalculator.Add("2,-4,-5");

        action.Should().Throw<Exception>()
            .And.Message.Should().Be("Negative not allowed : -4, -5");
    }

    [TestCase("", 0)]
    [TestCase("1", 1)]
    [TestCase("1,2", 2)]
    [TestCase("2.0,2.0", 4.0f)]
    [TestCase("//sep\n2sep3", 6)]
    public void multiply(string value, double expectedResult) {
        var stringCalculator = new StringCalculator();

        var result = stringCalculator.Multiply(value);

        result.Should().Be(expectedResult);
    }

}