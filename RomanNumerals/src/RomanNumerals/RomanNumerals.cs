namespace RomanNumerals;

public class RomanNumerals {

    private static readonly Dictionary<int, string> FormatedNumbers = new Dictionary<int, string>() {
        { 1, "I" },
        { 4, "IV" },
        { 5, "V"},
        { 9, "IX"},
        { 10, "X"},
        { 40, "XL"},
        { 50, "L"},
        { 90, "XC"},
        { 100, "C"}
    };

    public string Execute(int number) => number switch {
        _ when FormatedNumbers.ContainsKey(number) => FormatedNumbers[number],
        > 100 => Add(100, number),
        > 90 => Add(90, number),
        > 50 => Add(50, number),
        > 40 => Add(40, number),
        > 10 => Add(10, number),
        _ => Execute(number - 1) + FormatedNumbers[1]
    };

    private string Add(int value, int number) {
        return FormatedNumbers[value] + Execute(number - value);
    }
}