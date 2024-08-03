
Console.Write("Enter a year: ");

var input = Console.ReadLine();

var leapYears = new LeapYears.LeapYears();


var previousColor = Console.ForegroundColor;

try {
    var year = int.Parse(input);
    if (leapYears.IsLeapYear(year)) {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{year} is leap year!");
    } else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{year} is not leap year");
    }
} finally {
    Console.ForegroundColor = previousColor;
}
