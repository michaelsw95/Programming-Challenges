var engineSchematic = File.ReadAllLines("./input.txt").Select(line => line.ToCharArray()).ToArray();

var numbersNextToSymbols = new List<int>();

for (var i = 0; i < engineSchematic.Length; i++)
{
    var readIndex = 0;

    while (readIndex < engineSchematic[i].Length)
    {
        var candidateNumberCharacters = new List<char>();
        var hasNeighbouringSymbol = false;
        if (char.IsDigit(engineSchematic[i][readIndex]))
        {
            do
            {
                candidateNumberCharacters.Add(engineSchematic[i][readIndex]);

                if (!hasNeighbouringSymbol && GetHasNeighbouringSymbol(engineSchematic, i, readIndex))
                {
                    hasNeighbouringSymbol = true;
                }
                
                readIndex++;
            } while (readIndex < engineSchematic[i].Length && char.IsDigit(engineSchematic[i][readIndex]));

            if (hasNeighbouringSymbol)
            {
                numbersNextToSymbols.Add(int.Parse(new string(candidateNumberCharacters.ToArray())));
            }
        }

        readIndex++;
    }
}

Console.WriteLine($"Day 3 - Part 1: {numbersNextToSymbols.Sum()}");

bool GetHasNeighbouringSymbol(char[][] schematic, int xPosition, int yPosition)
{
    var positionsToCheck = new List<(int X, int Y)>
    {
        (xPosition + 1, yPosition),
        (xPosition - 1, yPosition),
        (xPosition, yPosition + 1),
        (xPosition, yPosition - 1),
        (xPosition + 1, yPosition + 1),
        (xPosition + 1, yPosition - 1),
        (xPosition - 1, yPosition + 1),
        (xPosition - 1, yPosition - 1),
    };

    return positionsToCheck.Any(position => PositionContainsSymbol(schematic, position.X, position.Y));
}

bool PositionContainsSymbol(char[][] schematic, int x, int y)
{
    if (x < 0 || y < 0 || x >= schematic.Length || y >= schematic[x].Length)
    {
        return false;
    }

    var charAtPosition = schematic[x][y];

    return charAtPosition != '.' && !char.IsDigit(charAtPosition);
}
