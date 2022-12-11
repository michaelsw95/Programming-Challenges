const int CrtWidth = 40;
const int CrtHeigth = 6;

var instructions = File.ReadAllLines("input.txt");

var instructionExecutionPosition = 0;
var registerValue = 1;
var cycleCount = 1;

var crtData = Enumerable.Range(0, CrtWidth * CrtHeigth).Select(_ => '.').ToArray();

var executingInstruction = (Value: 0, Wait: 0); 

while (instructionExecutionPosition < instructions.Length)
{
    if (executingInstruction.Wait > 0)
    {
        TryWriteToCrt(crtData, cycleCount, registerValue);

        executingInstruction.Wait--;
        registerValue += executingInstruction.Value;

        cycleCount++;
        continue;
    }

    var instruction = instructions[instructionExecutionPosition];
    
    if (instruction.StartsWith("addx"))
    {
        var instructionParts = instruction.Split(' ');
        executingInstruction = (Value: int.Parse(instructionParts[1]), Wait: 1);
    }

    crtData = TryWriteToCrt(crtData, cycleCount, registerValue);

    instructionExecutionPosition++;
    cycleCount++;
}

Console.WriteLine("Day 10 - Part 2");
var crtDataJoined = new string(crtData);
for (var i = 0; i < CrtHeigth; i++)
{
    Console.WriteLine(crtDataJoined.Substring(i * CrtWidth, CrtWidth));
}

char[] TryWriteToCrt(char[] crtOutput, int cycle, int register)
{
    var workingCycle = cycle;

    while (workingCycle > CrtWidth)
    {
        workingCycle -= CrtWidth;
    }

    var spritePosition = new int[3] { register - 1, register, register + 1 };
    if (spritePosition.Contains(workingCycle - 1))
    {
        crtOutput[cycle - 1] = '#';
        return crtOutput;
    }

    return crtOutput;
}
