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

    class VerbNounPair
    {
        public int Verb { get; set; }
        public int Noun { get; set; }
    }

    class Program
    {
        private const int _target = 19690720;

        static void Main(string[] args)
        {
            var program = File.ReadAllText(args[0])
                .Split(',')
                .Select(o => Convert.ToInt32(o))
                .ToArray();

            VerbNounPair result = null;
            for (var i = 0; i < program.Length; i++)
            {
                for (var j = 0; j < program.Length; j++)
                {
                    var newProgram = new int[program.Length];
                    Array.Copy(program, newProgram, newProgram.Length);

                    if (ResolveProgram(newProgram, i, j) == _target)
                    {
                        result = new VerbNounPair { Verb = i, Noun = j };
                        break;
                    }
                }

                if (result != null)
                {
                    break;
                }
            }

            Console.WriteLine("Day 2 - Part 2: " + (100 * result.Verb + result.Noun));
        }

        static int ResolveProgram(int[] program, int verb, int noun)
        {
            program[1] = verb;
            program[2] = noun;

            for (var i = 0; i < program.Length; i += 4)
            {
                var action = (Opcode)program[i];

                if (action == Opcode.Exit)
                {
                    break;
                }

                program[program[i + 3]] = action == Opcode.Addition ?
                    program[program[i + 1]] + program[program[i + 2]] :
                    program[program[i + 1]] * program[program[i + 2]];
            }

            return program[0];
        }
    }
}
