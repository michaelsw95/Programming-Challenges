using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Day_04_Part_02
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!TryParseCommandArguments(args, out int lowerBound, out int upperBound))
            {
                throw new ArgumentException("Please provide the lower and upper bound as cmd line arguments");
            }

            var validNumbers = new ConcurrentBag<int>();

            Parallel.For(lowerBound, upperBound, currentNumber => {
                var digits = GetDigits(currentNumber);

                var lastDigit = 0;

                foreach (var digit in digits)
                {
                    if (lastDigit > digit)
                    {
                        return;
                    }

                    lastDigit = digit;
                }

                var digitGrouping = digits.GroupBy(o => o)
                    .ToDictionary(o => o.Key, o => o.Count());

                if (digitGrouping.Any(o => o.Value == 2))
                {
                    validNumbers.Add(currentNumber);
                }
            });

            Console.WriteLine("Day 4 - Part 2: " + validNumbers.Count);
        }

        static List<int> GetDigits(int number)
        {
            var digits = new List<int>();

            while (number > 0)
            {
                var digit = number % 10;
                number /= 10;

                digits.Add(digit);
            }

            digits.Reverse();

            return digits;
        }

        static bool TryParseCommandArguments(string[] args, out int lowerBound, out int upperBound)
        {
            lowerBound = 0;
            upperBound = 0;

            return args.Length == 2 &&
                int.TryParse(args[0], out lowerBound) &&
                int.TryParse(args[1], out upperBound) &&
                lowerBound < upperBound;
        }
    }
}
