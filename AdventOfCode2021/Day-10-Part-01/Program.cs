var syntax = File.ReadAllLines("./input.txt");

var syntaxPoints = new Dictionary<char, int>()
{
    { ')', 3 },
    { ']', 57 }, 
    { '}', 1197 },
    { '>', 25137 }, 
};

var symbols = new Dictionary<char, char>()
{
    { '(', ')' },
    { '[', ']' }, 
    { '{', '}' },
    { '<', '>' }, 
};

var foundIllegalChars = new List<char>();

foreach (var line in syntax)
{
    var syntaxStack = new Stack<char>();
    var firstIllegalChar = default(char);

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
                firstIllegalChar = character;
                break;
            }
        }
    }

    if (firstIllegalChar != default(char))
    {
        foundIllegalChars.Add(firstIllegalChar);
    }
}

var charCounts = foundIllegalChars
    .GroupBy(character => character)
    .ToDictionary(charGrouping => charGrouping.Key, charGrouping => charGrouping.Count());

var total = 0;
foreach (var charCount in charCounts)
{
    total += syntaxPoints[charCount.Key] * charCount.Value;
}

Console.WriteLine($"Day 10 - Part 1: {total}");
