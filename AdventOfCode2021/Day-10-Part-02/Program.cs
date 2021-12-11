var syntax = File.ReadAllLines("./input.txt");

var syntaxPoints = new Dictionary<char, int>()
{
    { ')', 1 },
    { ']', 2 }, 
    { '}', 3 },
    { '>', 4 }, 
};

var symbols = new Dictionary<char, char>()
{
    { '(', ')' },
    { '[', ']' }, 
    { '{', '}' },
    { '<', '>' }, 
};

var lineScores = new List<long>();
foreach (var line in syntax)
{
    var syntaxStack = new Stack<char>();
    var lineWasCorrupted = false;

    foreach (var character in line)
    {
        if (symbols.ContainsKey(character))
        {
            syntaxStack.Push(character);
        }
        else
        {
            var expectedClose = symbols[syntaxStack.Pop()];

            if (expectedClose != character)
            {
                lineWasCorrupted = true;
                break;
            }
        }
    }

    if (lineWasCorrupted == false)
    {
        var charsNeededToCloseLine = syntaxStack
            .Select(unclosedChar => symbols[unclosedChar])
            .ToArray();

        var lineTotal = 0L;

        for (var i = 0; i < charsNeededToCloseLine.Length; i++)
        {
            lineTotal *= 5;
            lineTotal += syntaxPoints[charsNeededToCloseLine[i]];
        }

        lineScores.Add(lineTotal);
    }
}

var orderedScores = lineScores.OrderBy(score => score).ToArray();

var middle = Math.Floor((double)orderedScores.Length / 2);
 
Console.WriteLine($"Day 10 - Part 2: {orderedScores[(int)middle]}");
