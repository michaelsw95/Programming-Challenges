var universe = File
    .ReadAllLines("input.txt")
    .Select(ParseGalaxyMapInputLine)
    .ToList();

var emptyRows = GetEmptyRows(universe);
var emptyColumns = GetEmptyColumns(universe);

var galaxies = GetGalaxyPositions(universe);
var galaxyPairs = GetGalaxyPairs(galaxies);
var totalDistance = galaxyPairs.Sum(pair => CalculateDistance(emptyRows, emptyColumns, pair.One, pair.Two));

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

HashSet<int> GetEmptyRows(List<List<SpacePositionType>> map)
{
    var rows = new HashSet<int>();

    for (var i = 0; i < map.Count; i++)
    {
        if (map[i].All(position => position == SpacePositionType.Empty))
            rows.Add(i);
    }

    return rows;
}

HashSet<int> GetEmptyColumns(List<List<SpacePositionType>> map)
{
    var columns = new HashSet<int>();

    for (var i = 0; i < map[0].Count; i++)
    {
        var isEmpty = true;
        for (int j = 0; j < map.Count; j++)
        {
            if (map[j][i] == SpacePositionType.Galaxy)
            {
                isEmpty = false;
                break;
            }
        }
        
        if (isEmpty)
            columns.Add(i);
    }

    return columns;
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

long CalculateDistance(HashSet<int> virtualRows, HashSet<int> virtualColumns, Galaxy first, Galaxy second)
{
    var differenceX = Math.Abs(first.Position.X - second.Position.X);
    var differenceY = Math.Abs(first.Position.Y - second.Position.Y);
    
    var xMax = Math.Max(first.Position.X, second.Position.X);
    var yMax = Math.Max(first.Position.Y, second.Position.Y);

    var xMin = Math.Min(first.Position.X, second.Position.X);    
    var yMin = Math.Min(first.Position.Y, second.Position.Y);

    var virtualColumnsPassed = 0;
    for (var i = xMin; i < xMax; i++)
    {
        if (virtualColumns.Contains(i))
            virtualColumnsPassed++;
    }
    
    var virtualRowsPassed = 0;
    for (var i = yMin; i < yMax; i++)
    {
        if (virtualRows.Contains(i))
            virtualRowsPassed++;
    }

    const int spaceDistanceMultiplier = 1000000;
    return (differenceX - virtualColumnsPassed) +
           (differenceY - virtualRowsPassed) +
           (virtualColumnsPassed * spaceDistanceMultiplier) +
           (virtualRowsPassed * spaceDistanceMultiplier);
}

enum SpacePositionType
{
    Empty,
    Galaxy
}

record Galaxy(int Identifier, Position Position);
record Position(int X, int Y);
