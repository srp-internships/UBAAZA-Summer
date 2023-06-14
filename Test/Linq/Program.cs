
//Where
//int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

//var lowNums = from num in numbers
//              where num < 5
//              select num;

//Console.WriteLine("Numbers < 5:");
//foreach (var x in lowNums)
//{
//    Console.WriteLine(x);
//}
//Where
//string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
//
//var shortDigits = digits.Where((digit, index) => digit.Length < index);
//Console.WriteLine("Short digits:");
//foreach (var d in shortDigits)
//{
//    Console.WriteLine($"The word {d} is shorter than its value.");
//}
//select
//int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
//string[] strings = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

//var digitOddEvens = from n in numbers
//                    select new { Digit = strings[n], Even = (n % 2 == 0) };

//foreach (var d in digitOddEvens)
//{
//    Console.WriteLine($"The digit {d.Digit} is {(d.Even ? "even" : "odd")}.");
//}
//select и where
//int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
//string[] digits = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

//var lowNums = from n in numbers
//              where n < 5
//              select digits[n];

//Console.WriteLine("Numbers < 5:");
//foreach (var num in lowNums)
//{
//    Console.WriteLine(num);
//}
//Select from multiple input sequences
int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
int[] numbersB = { 1, 3, 5, 7, 8 };

var pairs = from a in numbersA
            from b in numbersB
            where a < b
            select (a, b);

Console.WriteLine("Pairs where a < b:");
foreach (var pair in pairs)
{
    Console.WriteLine($"{pair.a} is less than {pair.b}");
}