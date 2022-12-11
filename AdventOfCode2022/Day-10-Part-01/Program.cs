var instructions = File.ReadAllLines("input.txt");

var executionPosition = 0;
var signalStrength = new List<int>();
var registerValue = 1;
var cycleCount = 1;

var executingInstruction = (Value: 0, Wait: 0); 
while (executionPosition < instructions.Length || executingInstruction.Wait > 0)
{
    if (executingInstruction.Wait > 0)
    {
        executingInstruction.Wait--;

        signalStrength.Add(cycleCount * registerValue);

        if (executingInstruction.Wait == 0)
        {
            registerValue += executingInstruction.Value;
        }

        cycleCount++;
        continue;
    }

    var instruction = instructions[executionPosition];
    var instructionParts = instruction.Split(' ');
    
    if (instruction.StartsWith("addx"))
    {
        executingInstruction = (Value: int.Parse(instructionParts[1]), Wait: 1);
    }

    signalStrength.Add(cycleCount * registerValue);

    executionPosition++;
    cycleCount++;
}

var significantSignals = signalStrength[19] + signalStrength[59] + signalStrength[99] + signalStrength[139] + signalStrength[179] + signalStrength[219];

Console.WriteLine($"Day 10 - Part 1: {significantSignals}");
