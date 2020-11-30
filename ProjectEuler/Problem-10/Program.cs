using System;
using System.Linq;

namespace Euler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var maxNumber = 2000000;
            var sieve = MakePrimeSieve(maxNumber);
            var sumOfPrimes = GetSumOfSieve(sieve);

            Console.WriteLine($"Project Euler - Problem 10: {sumOfPrimes}");
        }

        public static long GetSumOfSieve(bool[] sieve)
        {
            var sum = default(long);

            for (var i = 2; i < sieve.Length; i++)
            {
                if (sieve[i])
                {
                    sum += i;
                }
            }

            return sum;
        }

        public static bool[] MakePrimeSieve(int max)
        {
            var isPrime = Enumerable
                .Repeat(true, max + 1)
                .ToArray();

            isPrime[0] = false;
            isPrime[1] = false;

            for (int i = 2; i <= max; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * 2; j <= max; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }

            return isPrime;
        }
    }
}
