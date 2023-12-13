var mapInput = File.ReadAllLines("input.txt");

var (parsedMap, startPosition) = ProcessMap(mapInput);

var path = GetMapPath(startPosition, parsedMap);

Console.WriteLine($"Day 10 - Part 1: {path.Count / 2}\n");

OutputMapToConsole(parsedMap, path, startPosition);

MapNodeType GetMapNodeType(char input) =>
    input switch
    {
        '|' => MapNodeType.Vertical,
        '-' => MapNodeType.Horizontal,
        'L' => MapNodeType.BendNorthEast,
        'J' => MapNodeType.BendNorthWest,
        '7' => MapNodeType.BendSouthWest,
        'F' => MapNodeType.BendSouthEast,
        '.' => MapNodeType.Ground,
        'S' => MapNodeType.Start,
        _ => throw new ArgumentException("Invalid input character")
    };

(MapNodeType[][], Position) ProcessMap(IEnumerable<string> lines)
{
    var lineArray = lines as string[] ?? lines.ToArray();
    var map = new MapNodeType[lineArray.Length][];
    var foundStartingPoint = new Position(-1, -1);
    
    for (var y = 0; y < lineArray.Length; y++)
    {
        map[y] = new MapNodeType[lineArray[y].Length];
        for (var x = 0; x < lineArray[y].Length; x++)
        {
            map[y][x] = GetMapNodeType(lineArray[y][x]);
            if (map[y][x] == MapNodeType.Start)
            {
                foundStartingPoint = new Position(x, y);
            }
        }
    }

    return (map, foundStartingPoint);
}

List<Position> GetMapPath(Position startingPoint, MapNodeType[][] map)
{
    var path = new List<Position> { startingPoint };
    
    foreach (var direction in Enum.GetValues<CardinalDirection>())
    {
        var candidateNext = direction switch
        {
            CardinalDirection.North => new Position(startingPoint.X, startingPoint.Y - 1),
            CardinalDirection.East => new Position(startingPoint.X + 1, startingPoint.Y),
            CardinalDirection.South => new Position(startingPoint.X, startingPoint.Y + 1),
            CardinalDirection.West => new Position(startingPoint.X - 1, startingPoint.Y),
            _ => throw new InvalidOperationException("Unknown direction")
        };

        if (IsPositionOnMap(candidateNext, map) && IsValidNextPosition(direction, map[candidateNext.Y][candidateNext.X]))
        {
            path.Add(candidateNext);
            break;
        }
    }

    var workingPosition = path.Last();

    do
    {
        var nextPosition = GetNextPosition(map, workingPosition, path);

        path.Add(nextPosition);
        workingPosition = nextPosition;
    } while (workingPosition != startPosition);

    return path;
}

bool IsValidNextPosition(CardinalDirection direction, MapNodeType candidateNext)
{
    var validNodeTypesForDirection = new Dictionary<CardinalDirection, List<MapNodeType>>
    {
        { CardinalDirection.North, new List<MapNodeType> { MapNodeType.Vertical, MapNodeType.BendSouthEast, MapNodeType.BendSouthWest } },
        { CardinalDirection.East, new List<MapNodeType> { MapNodeType.Horizontal, MapNodeType.BendNorthWest, MapNodeType.BendSouthWest } },
        { CardinalDirection.South, new List<MapNodeType> { MapNodeType.Vertical, MapNodeType.BendNorthEast, MapNodeType.BendNorthWest } },
        { CardinalDirection.West, new List<MapNodeType> { MapNodeType.Horizontal, MapNodeType.BendNorthEast, MapNodeType.BendSouthEast } }
    };

    var validNext = validNodeTypesForDirection[direction];

    return validNext.Contains(candidateNext);
}

bool IsPositionOnMap(Position position, MapNodeType[][] map) =>
    position.Y >= 0 &&
    position.Y < map.Length &&
    position.X >= 0 &&
    position.X < map[position.Y].Length;

Position GetNextPosition(MapNodeType[][] mapNodeTypes, Position currentPosition, List<Position> workingPath)
{
    var currentNodeType = mapNodeTypes[currentPosition.Y][currentPosition.X];
    var previousPosition = workingPath[^2];

    var (candidatePositionOne, candidatePositionTwo) = currentNodeType switch
    {
        MapNodeType.Horizontal or MapNodeType.Vertical => GetNextForLinear(currentPosition, currentNodeType),
        MapNodeType.BendNorthEast or MapNodeType.BendNorthWest or 
        MapNodeType.BendSouthWest or MapNodeType.BendSouthEast => GetNextForBend(currentPosition, currentNodeType),
        _ => throw new InvalidOperationException()
    };

    return candidatePositionOne.Equals(previousPosition) ? candidatePositionTwo : candidatePositionOne;
}

(Position candidatePositionOne, Position candidatePositionTwo) GetNextForLinear(Position current, MapNodeType nodeType)
    => nodeType switch
    {
        MapNodeType.Horizontal => (new Position(current.X - 1, current.Y), new Position(current.X + 1, current.Y)),
        MapNodeType.Vertical => (new Position(current.X, current.Y - 1), new Position(current.X, current.Y + 1)),
        _ => throw new InvalidOperationException()
    };

(Position candidatePositionOne, Position candidatePositionTwo) GetNextForBend(Position current, MapNodeType bendType) =>
    bendType switch
    {
        MapNodeType.BendNorthEast =>
            (new Position(current.X + 1, current.Y), new Position(current.X, current.Y - 1)),
        MapNodeType.BendNorthWest =>
            (new Position(current.X - 1, current.Y), new Position(current.X, current.Y - 1)),
        MapNodeType.BendSouthWest =>
            (new Position(current.X - 1, current.Y), new Position(current.X, current.Y + 1)),
        MapNodeType.BendSouthEast =>
            (new Position(current.X + 1, current.Y), new Position(current.X, current.Y + 1)),
        _ => throw new InvalidOperationException()
    };

void OutputMapToConsole(MapNodeType[][] map, List<Position> finalPath, Position startNode)
{
    var itemsOnPath = finalPath.ToHashSet();
    
    for (var y = 0; y < map.Length; y++)
    {
        for (var x = 0; x < map[y].Length; x++)
        {
            var current = new Position(x, y);

            if (current.Equals(startNode))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (itemsOnPath.Contains(current))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.Write(GetMapNodeSymbol(map[y][x]));
        }
        
        Console.WriteLine();
    }

    Console.ResetColor();
}

char GetMapNodeSymbol(MapNodeType nodeType)
    => nodeType switch
    {
        MapNodeType.Vertical => '|',
        MapNodeType.Horizontal => '-',
        MapNodeType.BendNorthEast => 'L',
        MapNodeType.BendNorthWest => 'J',
        MapNodeType.BendSouthWest => '7',
        MapNodeType.BendSouthEast => 'F',
        MapNodeType.Ground => '.',
        MapNodeType.Start => 'S',
        _ => throw new NotSupportedException()
    };

record Position(int X, int Y);

public enum MapNodeType
{
    Vertical = '|',
    Horizontal = '-',
    BendNorthEast = 'L',
    BendNorthWest = 'J',
    BendSouthWest = '7',
    BendSouthEast = 'F',
    Ground = '.',
    Start = 'S'
}

public enum CardinalDirection
{
    North,
    East,
    South,
    West
}
