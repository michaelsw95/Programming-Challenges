using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_05_Part_01
{
    enum Opcode 
    {
        Addition = 1,
        Multiplication = 2,
        GetInput = 3,
        GetOutput = 4,
        Exit = 99
    }

    enum ParameterMode
    {
        Positional = 0,
        Immediate = 1
    }

    class OpcodeInstruction
    {
        public Opcode Opcode { get; set; }
        public List<ParameterMode> ModesOfParameter { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var program = File.ReadAllText(args[0])
                .Split(',')
                .Select(o => Convert.ToInt32(o))
                .ToArray();

            Action<int> handleOutput = (int x) => Console.WriteLine("Out: " + x);

            Func<int> handleInput = () => {
                Console.WriteLine("Provide program input: ");
                var input = Console.ReadLine();
                return Convert.ToInt32(input);
            };

            var result = ResolveProgram(program, handleOutput, handleInput);

            Console.WriteLine("Day 5 - Part 1: " + result);
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

            return digits;
        }

        static int GetPointerMoveAmount(Opcode opcode)
        {
            return opcode == Opcode.Addition || opcode == Opcode.Multiplication ?
                4 : opcode == Opcode.GetOutput || opcode == Opcode.GetInput ?
                    2 : 1; 
        }

        static int GetParameter(int[] program, ParameterMode parameterMode, int index)
        {
            return parameterMode == ParameterMode.Positional ?
                program[program[index]] :
                program[index];
        }

        static OpcodeInstruction ParseProgramInstruction(int instruction)
        {
            var instructionAsString = instruction.ToString();

            if (instructionAsString.Length == 1)
            {
                return new OpcodeInstruction
                {
                    Opcode = (Opcode)Convert.ToInt32(instructionAsString),
                    ModesOfParameter = new List<ParameterMode>()
                }; 
            }

            var opcode = Convert.ToInt32(instructionAsString.Substring(instructionAsString.Length - 2, 2));
            
            var instructionDigits = GetDigits(instruction);

            return new OpcodeInstruction
            {
                Opcode = (Opcode)opcode,
                ModesOfParameter = instructionDigits
                    .Skip(2)
                    .Select(o => (ParameterMode)o)
                    .ToList()
            };
        }

        static int ResolveProgram(int[] program, Action<int> HandleOutput, Func<int> HandleInput)
        {
            var pointer = 0;
            var diagnosticCode = 0;
            OpcodeInstruction instruction = null;

            do
            {
                instruction = ParseProgramInstruction(program[pointer]);

                var firstParameterMode = instruction.ModesOfParameter.Count > 0 ?
                    instruction.ModesOfParameter[0] : ParameterMode.Positional;
                var secondParameterMode = instruction.ModesOfParameter.Count > 1 ?
                    instruction.ModesOfParameter[1] : ParameterMode.Positional;

                switch (instruction.Opcode)
                {
                    case Opcode.Addition:
                        program[program[pointer + 3]] =
                            GetParameter(program, firstParameterMode, pointer + 1) +
                            GetParameter(program, secondParameterMode, pointer + 2);
                        break;
                    case Opcode.Multiplication:
                        program[program[pointer + 3]] =
                            GetParameter(program, firstParameterMode, pointer + 1) *
                            GetParameter(program, secondParameterMode, pointer + 2);
                        break;
                    case Opcode.GetInput:
                        program[program[pointer + 1]] = HandleInput();
                        break;
                    case Opcode.GetOutput:
                        diagnosticCode = GetParameter(program, firstParameterMode, pointer + 1);
                        HandleOutput(diagnosticCode);
                        break;
                }

                pointer += GetPointerMoveAmount(instruction.Opcode);
            }
            while (instruction.Opcode != Opcode.Exit);

            return diagnosticCode;
        }
    }
}
