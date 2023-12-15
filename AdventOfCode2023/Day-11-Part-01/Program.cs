var universe = File
    .ReadAllLines("input.txt")
    .Select(ParseGalaxyMapInputLine)
    .ToList();

universe = ExpandSpaceRows(universe);
universe = ExpandSpaceColumns(universe);

var galaxies = GetGalaxyPositions(universe);
var galaxyPairs = GetGalaxyPairs(galaxies);
var totalDistance = galaxyPairs.Sum(pair => CalculateDistance(pair.One, pair.Two));

Console.WriteLine($"Day 11 - Part 1: {totalDistance}");

foreach (var row in universe)
{
    foreach (var column in row)
    {
        Console.Write(column == SpacePositionType.Empty ? '.' : '#');
    }

    Console.WriteLine();
}

List<SpacePositionType> ParseGalaxyMapInputLine(string inputLine) =>
    inputLine
        .ToCharArray()
        .Select(position => position == '#' ? SpacePositionType.Galaxy : SpacePositionType.Empty)
        .ToList();

List<List<SpacePositionType>> ExpandSpaceRows(List<List<SpacePositionType>> map)
{
    var currentRow = 0;

    while (currentRow < map.Count)
    {
        var isEmpty = map[currentRow].All(position => position == SpacePositionType.Empty);
    
        currentRow++;

        if (isEmpty)
        {
            var emptyRow = Enumerable.Repeat(SpacePositionType.Empty, map.First().Count).ToList();

            map.Insert(currentRow - 1, emptyRow);

            currentRow += 1;
        }
    }

    return map;
}

List<List<SpacePositionType>> ExpandSpaceColumns(List<List<SpacePositionType>> map)
{
    var currentColumn = 0;

    while (currentColumn < map[0].Count)
    {
        var isEmpty = map.All(row => row[currentColumn] != SpacePositionType.Galaxy);

        currentColumn++;

        if (isEmpty)
        {
            foreach (var row in map)
            {
                row.Insert(currentColumn - 1, SpacePositionType.Empty);
            }

            currentColumn++;
        }
    }

    return map;
}

List<Galaxy> GetGalaxyPositions(List<List<SpacePositionType>> universeMap)
{
    var galaxyPositions = new List<Galaxy>();

    var galaxiesFound = 0;

    for (var y = 0; y < universeMap.Count; y++)
    {
        for (var x = 0; x < universeMap[y].Count; x++)
        {
            if (universeMap[y][x] == SpacePositionType.Galaxy)
            {
                galaxiesFound++;
                
                var next = new Galaxy(galaxiesFound, new Position(x, y));
                galaxyPositions.Add(next);
            }
        }
    }

    return galaxyPositions;
}

List<(Galaxy One, Galaxy Two)> GetGalaxyPairs(List<Galaxy> allGalaxies)
{
    var pairs = new HashSet<(Galaxy One, Galaxy Two)>();

    foreach (var galaxyOne in allGalaxies)
    {
        foreach (var galaxyTwo in galaxies)
        {
            if (galaxyOne == galaxyTwo)
                continue;

            pairs.Add(galaxyOne.Identifier < galaxyTwo.Identifier ? (galaxyOne, galaxyTwo) : (galaxyTwo, galaxyOne));
        }
    }

    return pairs.ToList();
}

int CalculateDistance(Galaxy first, Galaxy second)
{
    var differenceX = Math.Abs(first.Position.X - second.Position.X);
    var differenceY = Math.Abs(first.Position.Y - second.Position.Y);

    return differenceX + differenceY;
}

enum SpacePositionType
{
    Empty,
    Galaxy
}

record Galaxy(int Identifier, Position Position);
record Position(int X, int Y);
