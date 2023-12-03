var engineSchematic = File.ReadAllLines("./input.txt")
    .Select(line => line.ToCharArray())
    .ToArray();

var gearNumberNeighbours = new List<GearWithNumberLocation>();

for (var i = 0; i < engineSchematic.Length; i++)
{
    var readIndex = 0;

    while (readIndex < engineSchematic[i].Length)
    {
        var candidateNumberCharacters = new List<char>();
        var neighbouringGearLocations = new List<(int X, int Y)>();
        if (char.IsDigit(engineSchematic[i][readIndex]))
        {
            do
            {
                candidateNumberCharacters.Add(engineSchematic[i][readIndex]);
                
                neighbouringGearLocations.AddRange(GetNeighbouringGearLocations(engineSchematic, i, readIndex));
                readIndex++;
            } while (readIndex < engineSchematic[i].Length && char.IsDigit(engineSchematic[i][readIndex]));

            if (neighbouringGearLocations.Any())
            {
                var number = int.Parse(new string(candidateNumberCharacters.ToArray()));

                var newGearLocations = neighbouringGearLocations
                    .Select(location => new GearWithNumberLocation(number, location.X, location.Y));
                
                gearNumberNeighbours.AddRange(newGearLocations);
            }
        }

        readIndex++;
    }
}

var totalGearRatio = gearNumberNeighbours
    .Distinct()
    .GroupBy(gearLocations => (gearLocations.gearX, gearLocations.gearY))
    .Where(grouping => grouping.Count() == 2)
    .Sum(gearGrouping => gearGrouping.Aggregate(1, (current, gear) => current * gear.number));

Console.WriteLine($"Day 3 - Part 1: {totalGearRatio}");

(int, int)[] GetNeighbouringGearLocations(char[][] schematic, int xPosition, int yPosition)
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

    return positionsToCheck
        .Where(position => PositionContainsGear(schematic, position.X, position.Y))
        .ToArray();
}

bool PositionContainsGear(char[][] schematic, int x, int y)
{
    if (x < 0 || y < 0 || x >= schematic.Length || y >= schematic[x].Length)
    {
        return false;
    }

    var charAtPosition = schematic[x][y];

    return charAtPosition == '*';
}

record GearWithNumberLocation(int number, int gearX, int gearY);