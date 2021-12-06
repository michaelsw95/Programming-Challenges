var ventLinesInput = File.ReadAllLines("input.txt");
var ventLines = new List<VentLine>(ventLinesInput.Length);

foreach (var line in ventLinesInput)
{
    var coords = line.Split(" -> ")
        .Select(lineParts => lineParts.Split(","))
        .SelectMany(values => values)
        .ToArray();

    ventLines.Add(new VentLine(
        int.Parse(coords[0]),
        int.Parse(coords[1]),
        int.Parse(coords[2]),
        int.Parse(coords[3])
    ));
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
        return GetRangeBetween(line.fromY, line.toY)
            .Select(yValue => (line.toX, yValue))
            .ToArray();
    }
    else if (line.fromY == line.toY)
    {
        return GetRangeBetween(line.fromX, line.toX)
            .Select(xValue => (xValue, line.toY))
            .ToArray();
    }

    var xCoords = GetRangeBetween(line.fromX, line.toX);
    var yCoords = GetRangeBetween(line.fromY, line.toY);

    var result = new (int X, int Y)[xCoords.Length];

    for (var i = 0; i < xCoords.Length; i++)
    {
        result[i] = (xCoords[i], yCoords[i]);
    }

    return result;
}

int[] GetRangeBetween(int start, int stop)
{
    if (start < stop)
    {
        return Enumerable.Range(start, (stop - start) + 1).ToArray();
    }

    var result = new List<int>();
    for (var i = start; i >= stop; i--)
    {
        result.Add(i);
    }

    return result.ToArray();
}

record VentLine(int fromX, int fromY, int toX, int toY);
