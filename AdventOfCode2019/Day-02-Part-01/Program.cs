using System;
using System.IO;
using System.Linq;

namespace Day_02_Part_01
{
    enum Opcode 
    {
        Addition = 1,
        Multiplication = 2,
        Exit = 99
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText(args[0])
                .Split(',')
                .Select(o => Convert.ToInt32(o))
                .ToArray();

            input[1] = 12;
            input[2] = 2;

            for (var i = 0; i < input.Length; i += 4)
            {
                var action = (Opcode)input[i];

                if (action == Opcode.Exit)
                {
                    break;
                }

                input[input[i + 3]] = action == Opcode.Addition ?
                    input[input[i + 1]] + input[input[i + 2]] :
                    input[input[i + 1]] * input[input[i + 2]];
            }

            Console.WriteLine("Day 2 - Part 1: " + input[0]);
        }
    }
}
