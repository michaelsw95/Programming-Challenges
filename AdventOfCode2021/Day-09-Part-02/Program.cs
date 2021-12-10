var heightMap = File.ReadAllLines("./input.txt")
    .Select(
        line => line
            .Select(number => Convert.ToInt32(number.ToString()))
            .ToArray()
    )
    .ToArray();

var lowPoints = new List<(int height, Position position)>();
for (var x = 0; x < heightMap.Length; x++)
{
    for (int y = 0; y < heightMap[0].Length; y++)
    {
        var current = heightMap[x][y];
        var position = new Position(x, y);

        if (IsPositionLowPoint(current, heightMap, position))
        {
            lowPoints.Add((current, position));
        }
    }
}

var largestBasins = lowPoints
    .Select(basin => GetSizeOfBasin(basin, heightMap))
    .OrderByDescending(size => size)
    .ToArray();

Console.WriteLine($"Day 9 - Part 2: {largestBasins[0] * largestBasins[1] * largestBasins[2]}");

bool IsPositionLowPoint(int current, int[][] heightMap, Position position) =>
    GetAllNeighbours(position, heightMap)
        .All(adjacent => adjacent.height > current);

int SafeReadHeightPosition(int[][] heightMap, int x, int y)
{
    if ((x < 0 || x > heightMap.Length - 1) || (y < 0 || y > heightMap[0].Length - 1))
    {
        return int.MaxValue;
    }

    return heightMap[x][y];
}

int GetSizeOfBasin((int height, Position position) basinLowPoint, int[][] heightMap) =>
    GetNeighboursThatAreLarger(
        basinLowPoint.height,
        basinLowPoint.position,
        new List<(int height, Position position)>() { basinLowPoint },
        heightMap)
        .Count();

List<(int height, Position position)> GetNeighboursThatAreLarger(
    int current,
    Position position,
    List<(int height, Position position)> visited,
    int[][] heightMap)
{
    const int maxHeight = 9;
    var newLargerNeighbours = GetAllNeighbours(position, heightMap)
        .Where(heightPosition => !visited.Contains(heightPosition))
        .Where(heightPosition => heightPosition.height > current && heightPosition.height < maxHeight)
        .ToArray();

    if (newLargerNeighbours.Length == 0)
    {
        return visited;
    }

    var newRange = new List<(int height, Position Position)>();
    visited.AddRange(newLargerNeighbours);

    foreach (var neighbour in newLargerNeighbours)
    {
        GetNeighboursThatAreLarger(
            neighbour.height,
            neighbour.position,
            visited,
            heightMap);
    }

    return visited;
}

(int height, Position position)[] GetAllNeighbours(Position position, int[][] heightMap) =>
    new (int height, Position position)[] {
        (SafeReadHeightPosition(heightMap, position.x - 1, position.y), new Position(position.x - 1, position.y)),
        (SafeReadHeightPosition(heightMap, position.x + 1, position.y), new Position(position.x + 1, position.y)),
        (SafeReadHeightPosition(heightMap, position.x, position.y - 1), new Position(position.x, position.y - 1)),
        (SafeReadHeightPosition(heightMap, position.x, position.y + 1), new Position(position.x, position.y + 1))
    };

record Position(int x, int y);