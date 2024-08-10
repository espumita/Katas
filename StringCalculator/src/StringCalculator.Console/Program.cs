Console.Write("Enter string:");

var value = Console.ReadLine();

var stringCalculator = new StringCalculator.StringCalculator();

var result = stringCalculator.Add(value);

Console.WriteLine(result);