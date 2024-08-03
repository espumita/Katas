namespace FizzBuzz;

public class FizzBuzz {

    public string Execute(int number) => (IsMultipleOfThree(number), IsMultipleOfFive(number)) switch {
        (true, true) => "FizzBuzz",
        (true, false) => "Fizz",
        (false, true) => "Buzz",
        _ => number.ToString()
    };

    private static bool IsMultipleOfThree(int number) {
        return number % 3 == 0;
    }

    private static bool IsMultipleOfFive(int number) {
        return number % 5 == 0;
    }
}