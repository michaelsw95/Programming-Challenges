using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

var instructionExectutor = new Dictionary<string, Func<Program, int, Program>>(3)
{
    {
        "acc", (program, instructionAmount) => program with
        { 
            Accumulator = program.Accumulator + instructionAmount,
            Position = program.Position + 1
        }
    },
    {
        "jmp", (program, instructionAmount) => program with
        { 
            Position = program.Position + instructionAmount
        }
    },
    {
        "nop", (program, _) => program with
        { 
            Position = program.Position + 1
        }
    }
};

var instructions = File
    .ReadAllLines("input.txt")
    .ToArray();

var programState = new Program
{
    Position = 0,
    Accumulator = 0
};

var visitedInstructions = new HashSet<int>(instructions.Length);

while (!visitedInstructions.Contains(programState.Position))
{
    visitedInstructions.Add(programState.Position);
    
    var instruction = GetParsedInstruction(instructions[programState.Position]);

    programState = instructionExectutor[instruction.Type](programState, instruction.Amount);
}

Console.WriteLine($"Day 8 - Part 1: {programState.Accumulator}");

(string Type, int Amount) GetParsedInstruction(string rawInstruction)
{
    var splitInstruction = rawInstruction
        .Replace("+", string.Empty)
        .Split(" ");
    
    return (splitInstruction.First(), int.Parse(splitInstruction.Last()));
}

record Program 
{
    public int Position { get; init; }
    public int Accumulator { get; init; }
}