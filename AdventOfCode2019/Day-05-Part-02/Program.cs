using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Day_05_Part_02.Models;

namespace Day_05_Part_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var program = File.ReadAllText(args[0])
                .Split(',')
                .Select(o => Convert.ToInt32(o))
                .ToArray();

            Action<int> handleOutput = (int o) => Console.WriteLine("Out: " + o);

            Func<int> handleInput = () => {
                Console.WriteLine("Provide program input: ");
                var input = Console.ReadLine();
                return Convert.ToInt32(input);
            };

            var result = ResolveProgram(program, handleOutput, handleInput);

            Console.WriteLine("Day 5 - Part 2: " + result);
        }

        public static List<int> GetDigits(int number)
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

        public static int GetPointerMoveAmount(Opcode opcode)
        {
            switch(opcode)
            {
                case Opcode.Addition:
                case Opcode.Multiplication:
                case Opcode.Equals:
                case Opcode.LessThan:
                    return 4;
                case Opcode.JumpIfTrue:
                case Opcode.JumpIfFalse:
                    return 3;
                case Opcode.GetInput:
                case Opcode.GetOutput:
                    return 2;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported Opcode");
            }
        }

        public static int GetParameter(int[] program, ParameterMode parameterMode, int index)
        {
            var result = 0;

            if (parameterMode == ParameterMode.Positional)
            {
                result = program[program[index]];
            }
            else if (parameterMode == ParameterMode.Immediate)
            {
                result = program[index];
            }

            return result;
        }

        public static OpcodeInstruction ParseProgramInstruction(int instruction)
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

        public static int ResolveProgram(int[] program, Action<int> HandleOutput, Func<int> HandleInput)
        {
            var pointer = 0;
            var diagnosticCode = 0;
            OpcodeInstruction instruction = null;

            do
            {
                var jump = false;
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
                    case Opcode.JumpIfTrue:
                        if (GetParameter(program, firstParameterMode, pointer + 1) != 0)
                        {
                            pointer = GetParameter(program, secondParameterMode, pointer + 2);
                            jump = true;
                        }
                        break;
                    case Opcode.JumpIfFalse:
                        if (GetParameter(program, firstParameterMode, pointer + 1) == 0)
                        {
                            pointer = GetParameter(program, secondParameterMode, pointer + 2);
                            jump = true;
                        }
                        break;
                    case Opcode.LessThan:
                        program[program[pointer + 3]] =
                            GetParameter(program, firstParameterMode, pointer + 1) <
                            GetParameter(program, secondParameterMode, pointer + 2) ? 1 : 0;
                        break;
                    case Opcode.Equals:
                        program[program[pointer + 3]] =
                            GetParameter(program, firstParameterMode, pointer + 1) ==
                            GetParameter(program, secondParameterMode, pointer + 2) ? 1 : 0;
                        break;
                    case Opcode.Exit:
                        return diagnosticCode;
                }

                if (!jump)
                {
                    pointer += GetPointerMoveAmount(instruction.Opcode);
                }
            }
            while (pointer < program.Length);

            return diagnosticCode;
        }
    }
}
