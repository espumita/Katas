namespace LeapYears;

public class LeapYears {
    public bool IsLeapYear(int year) {
        return IsDivisibleBy(400, year)
               || !IsDivisibleBy(100, year)
               && IsDivisibleBy(4, year);
    }

    private static bool IsDivisibleBy(int value, int year) {
        return year % value == 0;
    }
}