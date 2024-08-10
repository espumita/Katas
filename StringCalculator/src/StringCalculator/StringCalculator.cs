using System.Text.RegularExpressions;

namespace StringCalculator;

public class StringCalculator {
    public double Add(string value) {
        if (string.IsNullOrEmpty(value)) return 0;
        return NumbersFrom(value)
            .Sum();
    }

    public double Multiply(string value) {
        if (string.IsNullOrEmpty(value)) return 0;
        return NumbersFrom(value)
            .Aggregate((total, next) => total * next);
    }

    public IEnumerable<double> NumbersFrom(string value) {
        var numbers = RemoveSeparators(value);
        CheckForNegativeNumbers(numbers);
        return numbers.Select(double.Parse);
    }

    private static string[] RemoveSeparators(string value) {
        var isUsingCustomSeparator = IsUsingCustomSeparator(value);
        var separators = Separators(value, isUsingCustomSeparator);
        var valueWithoutFirstLine = RemoveFirstLine(value, isUsingCustomSeparator);
        CheckSeparators(valueWithoutFirstLine, separators);
        return valueWithoutFirstLine.Split(separators.ToArray(), StringSplitOptions.None);
    }

    private static bool IsUsingCustomSeparator(string value) {
        return value.StartsWith("//");
    }

    private static List<string> Separators(string value, bool isUsingCustomSeparator) {
        if (!isUsingCustomSeparator) return [",", "\n"];
        var indexOfEndOfLine = value.IndexOf('\n');
        return [value.Substring(2, indexOfEndOfLine - 2)];
    }

    private static string RemoveFirstLine(string value, bool isUsingCustomSeparator) {
        if (!isUsingCustomSeparator) return value;
        var indexOfEndOfLine = value.IndexOf('\n');
        return value.Substring(indexOfEndOfLine + 1);
    }

    private static void CheckSeparators(string value, List<string> separators) {
        if (StringNotEndsWithANumber(value)) throw new Exception("Number expected but EOF found.");
        var i = 0;
        while (i < value.Length - 1) {
            var subString = value.Substring(i);
            if (IsUsingCustomSeparator(separators) && AnotherSeparatorIsPresent(value, separators, subString, i))
                throw new Exception($"'{separators.Single()}' expected but '{value[i]}' found at position {i}.");
            var nextSeparator = separators.FirstOrDefault(subString.StartsWith);
            if (SeparatorIsPresent(nextSeparator)) {
                CheckIfNumberExistsAfterSeparator(value, separators, i, nextSeparator);
                i += nextSeparator.Length;
            } else {
                i++;
            }
        }
    }

    private static bool StringNotEndsWithANumber(string value) {
        return !Regex.IsMatch(value.Substring(value.Length - 1), "[0-9]");
    }

    private static bool IsUsingCustomSeparator(List<string> separators) {
        return separators.Count == 1;
    }

    private static bool AnotherSeparatorIsPresent(string value, List<string> separators, string subString, int i) {
        return !subString.StartsWith(separators.Single())
                && !Regex.IsMatch(value[i].ToString(), @"^[0-9\.]");
    }

    private static bool SeparatorIsPresent(string? nextSeparator) {
        return !string.IsNullOrEmpty(nextSeparator);
    }

    private static void CheckIfNumberExistsAfterSeparator(string value, List<string> separators, int i, string nextSeparator) {
        var subStringAfterSeparator = value.Substring(i + nextSeparator.Length);
        var secondSeparator = separators.FirstOrDefault(subStringAfterSeparator.StartsWith);
        if (SeparatorIsPresent(secondSeparator))
            throw new Exception($"Number expected but '{value[i + nextSeparator.Length]}' found at position {i + nextSeparator.Length}.");
    }

    private void CheckForNegativeNumbers(string[] numbers) {
        var negativeNumbers = numbers.Where(number => number.StartsWith('-'));
        if (negativeNumbers.Any()) throw new Exception($"Negative not allowed : {string.Join(", ", negativeNumbers)}");
    }
}