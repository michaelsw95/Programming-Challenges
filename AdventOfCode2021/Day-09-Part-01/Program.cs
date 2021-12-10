var heightMap = File.ReadAllLines("./input.txt")
    .Select(
        line => line
            .Select(number => Convert.ToInt32(number.ToString()))
            .ToArray()
    )
    .ToArray();

var lowPoints = new List<(int position, int x, int y)>();
for (var x = 0; x < heightMap.Length; x++)
{
    for (int y = 0; y < heightMap[0].Length; y++)
    {
        var current = heightMap[x][y];

        if (IsPositionLowPoint(current, heightMap, x, y))
        {
            lowPoints.Add((current, x, y));
        }
    }
}

var riskLevel = lowPoints.Sum(height => height.position + 1);

Console.WriteLine($"Day 9 - Part 1: {riskLevel}");

bool IsPositionLowPoint(int current, int[][] heightMap, int x, int y)
{
    var up = SafeReadHeightPosition(heightMap, x - 1, y);
    var down = SafeReadHeightPosition(heightMap, x + 1, y);
    var left = SafeReadHeightPosition(heightMap, x, y - 1);
    var right = SafeReadHeightPosition(heightMap, x, y + 1);

    return new int[] { up, down, left, right }.All(adjacent => adjacent > current);
}

int SafeReadHeightPosition(int[][] heightMap, int x, int y)
{
    if ((x < 0 || x > heightMap.Length - 1) || (y < 0 || y > heightMap[0].Length - 1))
    {
        return int.MaxValue;
    }

    return heightMap[x][y];
}
