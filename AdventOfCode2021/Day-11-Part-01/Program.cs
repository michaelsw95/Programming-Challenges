const int targetNumberOfSteps = 100;
const int flashThreshold = 9;

var octopusEnergyLevels = File.ReadAllLines("input.txt")
    .Select(line => line.Select(energy => int.Parse(energy.ToString())).ToArray())
    .ToArray();

var numberOfFlashes = GetNumberOfFlashesAfterGenerations(octopusEnergyLevels, targetNumberOfSteps);

Console.WriteLine($"Day 11 - Part 1: {numberOfFlashes}");

int GetNumberOfFlashesAfterGenerations(int[][] octopusEnergyLevels, int targetGenerations)
{
    var mapState = new OctopusMapState(octopusEnergyLevels, new HashSet<Position>());
    var totalFlashes = 0;

    for (int generation = 0; generation < targetGenerations; generation++)
    {
        var readyToFlash = new List<Position>();

        for (var x = 0; x < mapState.energyMap[0].Length; x++)
        {
            for (var y = 0; y < mapState.energyMap.Length; y++)
            {
                mapState.energyMap[x][y] += 1; 

                if (mapState.energyMap[x][y] > flashThreshold)
                {
                    mapState.hasFlashedThisGeneration.Add(new Position(x, y));
                    readyToFlash.Add(new Position(x, y));
                }
            }
        }

        foreach (var octopus in readyToFlash)
        {
            mapState = IncrementNeighbours(octopus, mapState);
        }

        totalFlashes += mapState.hasFlashedThisGeneration.Count();

        foreach (var position in mapState.hasFlashedThisGeneration)
        {
            mapState.energyMap[position.X][position.Y] = 0;
        }

        mapState.hasFlashedThisGeneration.Clear();
    }

    return totalFlashes;
}

OctopusMapState IncrementNeighbours(Position centre, OctopusMapState mapState)
{
    var positionsToCheck = GetPositionsToIncrement(centre, mapState);
    var readyToFlash = new List<Position>();

    foreach (var position in positionsToCheck)
    {
        if (mapState.hasFlashedThisGeneration.Contains(position))
        {
            continue;
        }

        mapState.energyMap[position.X][position.Y] += 1;

        if (mapState.energyMap[position.X][position.Y] > flashThreshold)
        {
            mapState.hasFlashedThisGeneration.Add(position);
            readyToFlash.Add(position);
        }
    }

    foreach (var position in readyToFlash)
    {
        mapState = IncrementNeighbours(position, mapState);
    }

    return mapState;
}

Position[] GetPositionsToIncrement(Position centre, OctopusMapState mapState) =>
    new List<Position>()
    {
        centre with { X = centre.X - 1 }, centre with { X = centre.X + 1 },
        centre with { Y = centre.Y - 1 }, centre with { Y = centre.Y + 1 },
        centre with { Y = centre.Y - 1, X = centre.X - 1 }, centre with { Y = centre.Y + 1, X = centre.X + 1 },
        centre with { Y = centre.Y - 1, X = centre.X + 1 }, centre with { Y = centre.Y + 1, X = centre.X - 1 },
    }
    .Where(position => IsValidPosition(position, mapState.energyMap.Length))
    .ToArray();

bool IsValidPosition(Position position, int mapLength) =>
    position.X >= 0 && position.X < mapLength &&
    position.Y >= 0 && position.Y < mapLength;

record Position(int X, int Y);
record OctopusMapState(int[][] energyMap, HashSet<Position> hasFlashedThisGeneration);
