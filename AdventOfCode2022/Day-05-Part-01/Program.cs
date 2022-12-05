using System.Text.RegularExpressions;

var rawInput = File.ReadAllLines("./input.txt");
var columns = new List<List<string>>();
var endOfColumnMapping = 0;
for (var i = 0; i < rawInput.Length; i++)
{
    if (char.IsNumber(rawInput[i][1]))
    {
        var maxColumns = rawInput[i].Split(' ').Where(character => !string.IsNullOrEmpty(character)).Last();

        for (var j = 0; j < Convert.ToInt32(maxColumns); j++)
        {
            columns.Add(new List<string>());
        }

        endOfColumnMapping = i;

        break;
    }
}

for (var i = 0; i < endOfColumnMapping; i++)
{
    var line = rawInput[i];
    for (var j = 0; j < line.Length; j++)
    {
        if (line[j] == '[')
        {
            columns[j / 4].Add(line.Substring(j + 1, 1));
        }
    }
}

for (var i = endOfColumnMapping + 2; i < rawInput.Length; i++)
{
    var matches = Regex.Matches(rawInput[i], @"\d+")
        .Select(match => Convert.ToInt32(match.Value))
        .ToArray();

    var action = new MoveInstructions(Convert.ToInt32(matches[1]) - 1, matches[2] - 1, matches[0]);

    var toMove = columns[action.FromColumn].Take(action.Amount);
    
    foreach (var itemToMove in toMove)
    {
        columns[action.ToColumn].Insert(0, itemToMove);
    }

    columns[action.FromColumn].RemoveRange(0, action.Amount);
}

var result = new string(columns.Select(column => column.First().First()).ToArray());

Console.WriteLine($"Day 5 - Part 1: {result}");

record MoveInstructions(int FromColumn, int ToColumn, int Amount);
