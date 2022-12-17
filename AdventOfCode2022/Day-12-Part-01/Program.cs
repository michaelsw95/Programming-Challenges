using System.Text;

var map = File.ReadAllLines("./input.txt")
    .Select(line => line.Select(linePart => new MapSquare(linePart)).ToArray())
    .ToArray();

var start = default(Position);
var end = default(Position);
for (var i = 0; i < map.Length; i++)
{
    for (var j = 0; j < map[0].Length; j++)
    {
        if (map[i][j].Type == MapSquareType.Exit)
        {
            end = new Position(i, j);
        }
        else if (map[i][j].Type == MapSquareType.Entry)
        {
            start = new Position(i, j);
        }
    }
}

var shortestPath = GetShortestRoute(map, start, end);

var output = new StringBuilder();
for (var i = 0; i < map.Length; i++)
{
    for (var j = 0; j < map[i].Length; j++)
    {
        if (shortestPath.Contains(new Position(i, j)))
        {
            var height = map[i][j].Height;
            var toPrepend = height < 10 ? "0" : string.Empty;

            output.Append($"{toPrepend}{height} ");
        }
        else if (map[i][j].Type == MapSquareType.Exit)
        {
            output.Append("E  ");
        }
        else
        {
            output.Append("-- ");
        }
    }

    output.AppendLine();
}

Console.WriteLine($"Day 12 - Part 1: {shortestPath.Count} \n");
Console.WriteLine(output);

List<Position> GetShortestRoute(MapSquare[][] map, Position startingPosition, Position endPoisition)
{
    var toExplore = new Queue<(Position Current, Position Parent)>();
    var visited = new Dictionary<Position, Position>();

    toExplore.Enqueue((startingPosition, default));

    while (toExplore.Count > 0)
    {
        var activePosition = toExplore.Dequeue();
        var current = activePosition.Current;
        var parent = activePosition.Parent;

        if (visited.ContainsKey(current))
        {
            continue;
        }

        var candidatePositions = new List<Position>
        {
            new Position(current.X + 1, current.Y),
            new Position(current.X - 1, current.Y),
            new Position(current.X, current.Y + 1),
            new Position(current.X, current.Y - 1)
        };

        foreach (var nextPosition in candidatePositions)
        {
            if (!visited.ContainsKey(nextPosition) &&
                IsSafePosition(map, nextPosition) &&
                IsTraversablePosition(map, current, nextPosition))
            {
                toExplore.Enqueue((nextPosition, current));
            }
        }

        visited.Add(current, parent);
    }

    var endPositionInNodes = visited.Single(node => node.Key == endPoisition);

    var pathBack = new List<Position> { endPositionInNodes.Value };

    var nextParent = endPositionInNodes.Value;
    while(pathBack.Last() != startingPosition)
    {
        nextParent = visited[nextParent];
        pathBack.Add(nextParent);
    }

    pathBack.Reverse();

    return pathBack;
}

bool IsSafePosition(MapSquare[][] map, Position position) => 
    position.X >= 0 &&
    position.X < map.Length &&
    position.Y >= 0 && position.Y < map[0].Length;

bool IsTraversablePosition(MapSquare[][] map, Position currentPosition, Position candidateNextPosition)
{
    var currentSquareHeight = map[currentPosition.X][currentPosition.Y].Height;
    var candidateSquareHeight = map[candidateNextPosition.X][candidateNextPosition.Y].Height;

    return candidateSquareHeight <= currentSquareHeight || candidateSquareHeight - currentSquareHeight <= 1;
}

class MapSquare
{
    public MapSquareType Type { get; set; }
    public int Height { get; set; }

    private static char[] _heightMapSymbols = Enumerable
        .Range('a', 'z' - 'a' + 1)
        .Select(letter => (Char)letter)
        .ToArray();

    public MapSquare(char inputMapSquare)
    {
        Type = inputMapSquare switch
        {
            'S' => MapSquareType.Entry,
            'E' => MapSquareType.Exit,
            _ => MapSquareType.NormalMapSquare
        };

        Height = Type switch
        {
            MapSquareType.Entry => 0,
            MapSquareType.Exit => Convert.ToInt32(_heightMapSymbols.Length),
            _ => Convert.ToInt32(Array.IndexOf(_heightMapSymbols, inputMapSquare))
        };
    }
}

enum MapSquareType
{
    Entry,
    Exit,
    NormalMapSquare
}

record Position(int X, int Y);
