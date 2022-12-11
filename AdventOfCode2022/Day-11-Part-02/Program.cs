using System.Text.RegularExpressions;

const int MonkeyDefinitionSize = 7;
const int NumberOfRounds = 10000;

var input = File.ReadAllLines("./input.txt");
var monkeys = new List<Monkey>(input.Length / MonkeyDefinitionSize);

for (var i = 0; i < input.Length; i += MonkeyDefinitionSize)
{
    monkeys.Add(new Monkey
    {
        ItemWorryLevel = input[i + 1].Replace("  Starting items: ", "").Split(',').Select(worry => long.Parse(worry)).ToList(),
        Operation = new Operation(input[i + 2].Replace("  Operation: new = ", "")),
        TestShouldBeDividableBy = GetNumberFromInput(input[i + 3]),
        IfTruePassToMonkeyId = GetNumberFromInput(input[i + 4]),
        IfFalsePassToMonkeyId = GetNumberFromInput(input[i + 5]),
    });
}

var safeModuloDivisor = 1;
foreach (var divisior in monkeys.Select(monkey => monkey.TestShouldBeDividableBy))
{
    safeModuloDivisor *= divisior;
}

for (var i = 0; i < NumberOfRounds; i++)
{
    foreach (var currentMonkey in monkeys)
    {
        foreach (var itemToInspect in currentMonkey.ItemWorryLevel)
        {
            var currentWorryLevel = currentMonkey.Operation.Evaluate(itemToInspect) % safeModuloDivisor;

            var newMonkey = currentWorryLevel % currentMonkey.TestShouldBeDividableBy == 0 ?
                currentMonkey.IfTruePassToMonkeyId :
                currentMonkey.IfFalsePassToMonkeyId;

            monkeys[newMonkey].ItemWorryLevel.Add(currentWorryLevel);

            currentMonkey.NumberOfItemsInspected++;
        }

        currentMonkey.ItemWorryLevel.Clear();
    }
}

var monkeyBussiness = monkeys.OrderByDescending(monkey => monkey.NumberOfItemsInspected).Take(2).ToArray();

Console.WriteLine($"Day 11 - Part 1: {monkeyBussiness[0].NumberOfItemsInspected * monkeyBussiness[1].NumberOfItemsInspected}");

int GetNumberFromInput(string input) => int.Parse(Regex.Matches(input, @"\d+").Single().Value);

class Monkey
{
    public List<long> ItemWorryLevel { get; set; }
    public int IfTruePassToMonkeyId { get; set; }
    public int IfFalsePassToMonkeyId { get; set; }
    public Operation Operation { get; set; }
    public int TestShouldBeDividableBy { get; set; }
    public long NumberOfItemsInspected { get; set; }
}

class Operation
{
    public OperationType Type;
    public OperationPart LeftHandSide;
    public OperationPart RightHandSide;

    public Operation(string operationRule)
    {
        var operationParts = operationRule.Split(' ');

        Type = operationParts[1] switch
        {
            "*" => OperationType.Multiply,
            "+" => OperationType.Add,
            _ => throw new NotSupportedException()
        };

        LeftHandSide = new OperationPart(operationParts[0]);
        RightHandSide = new OperationPart(operationParts[2]);
    }

    public long Evaluate(long variableValue)
    {
        var leftHandSide = LeftHandSide.Type == OperationPartType.Number ? LeftHandSide.Value : variableValue;
        var rightHandSide = RightHandSide.Type == OperationPartType.Number ? RightHandSide.Value : variableValue;

        return Type switch
        {
            OperationType.Multiply => leftHandSide * rightHandSide,
            OperationType.Add => leftHandSide + rightHandSide,
            _ => throw new NotSupportedException()
        };
    }
}

class OperationPart
{
    public long Value { get; set; }
    public OperationPartType Type { get; set; }

    public OperationPart(string operationPart)
    {
        if (int.TryParse(operationPart, out var value))
        {
            Value = value;
            Type = OperationPartType.Number;
        }
        else
        {
            Type = OperationPartType.Variable;
        }
    }
}

enum OperationPartType
{
    Number,
    Variable
}

enum OperationType
{
    Multiply,
    Add,
    Subtract,
    Divide
}
