var ventLinesInput = File.ReadAllLines("input.txt");
var ventLines = new List<VentLine>(ventLinesInput.Length);

foreach (var line in ventLinesInput)
{
    var coords = line.Split(" -> ")
        .Select(lineParts => lineParts.Split(","))
        .SelectMany(values => values)
        .ToArray();

    var newVentLine = new VentLine(
        int.Parse(coords[0]),
        int.Parse(coords[1]),
        int.Parse(coords[2]),
        int.Parse(coords[3])
    );

    if (newVentLine.fromX == newVentLine.toX || newVentLine.fromY == newVentLine.toY)
    {
        ventLines.Add(newVentLine);
    }
}

var ventCoordinateVisits = new HashSet<(int X, int Y)>();
var collisionPoints = new HashSet<(int X, int Y)>();
foreach (var line in ventLines)
{
    var newCoordinatesToCheck = GetVentPathCoordinates(line);

    foreach (var coord in newCoordinatesToCheck)
    {
        if (ventCoordinateVisits.Contains(coord)) 
        {
            collisionPoints.Add(coord);
        }
        else
        {
            ventCoordinateVisits.Add(coord);
        }
    }
}

Console.WriteLine($"Day 5 - Part 2: {collisionPoints.Count}");

(int X, int Y)[] GetVentPathCoordinates(VentLine line)
{
    if (line.fromX == line.toX)
    {
        var larger = Math.Max(line.fromY, line.toY);
        var smaller = Math.Min(line.fromY, line.toY);

        return Enumerable.Range(smaller, (larger - smaller) + 1)
            .Select(yValue => (line.toX, yValue))
            .ToArray();
    }
    else if (line.fromY == line.toY)
    {
        var larger = Math.Max(line.fromX, line.toX);
        var smaller = Math.Min(line.fromX, line.toX);

        return Enumerable.Range(smaller, (larger - smaller) + 1)
            .Select(xValue => (xValue, line.toY))
            .ToArray();
    }

    throw new NotSupportedException();
}

record VentLine(int fromX, int fromY, int toX, int toY);
