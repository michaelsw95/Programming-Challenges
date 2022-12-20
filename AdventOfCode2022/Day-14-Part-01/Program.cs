var rockPaths = File
    .ReadAllLines("input.txt")
    .Select(input => {
        var rockPathParts = input.Split(" -> ").Select(pathDefinition => {
            var rockStartAndEnd = pathDefinition.Split(',');
            var x = int.Parse(rockStartAndEnd.First());
            var y = int.Parse(rockStartAndEnd.Last());

            return new MapNode(x, y);
        });

        return rockPathParts.ToArray();
    });

var map = new Dictionary<MapNode, NodeType>();

var mapMiniumX = int.MaxValue;
var mapMiniumY = int.MaxValue;
var mapMaximumX = 0;
var mapMaximumY = 0;

foreach (var path in rockPaths)
{    
    for (var i = 0; i < path.Length - 1; i++)
    {
        var positionOne = path[i];
        var positionTwo = path[i + 1];

        var newMinX = Math.Min(positionOne.X, positionTwo.X);
        var newMinY = Math.Min(positionOne.Y, positionTwo.Y);
        var newMaxX = Math.Max(positionOne.X, positionTwo.X);
        var newMaxY = Math.Max(positionOne.Y, positionTwo.Y);

        mapMiniumX = Math.Min(newMinX, mapMiniumX);
        mapMiniumY = Math.Min(newMinY, mapMiniumY);
        mapMaximumX = Math.Max(newMaxX, mapMaximumX);
        mapMaximumY = Math.Max(newMaxY, mapMaximumY);

        if (positionOne.X == positionTwo.X)
        {
            var start = Math.Min(positionOne.Y, positionTwo.Y);
            var end = Math.Max(positionOne.Y, positionTwo.Y);

            for (var j = start; j < end + 1; j++)
            {
                map[new MapNode(positionOne.X, j)] = NodeType.Rock;
            }
        }
        else if (positionOne.Y == positionTwo.Y)
        {
            var start = Math.Min(positionOne.X, positionTwo.X);
            var end = Math.Max(positionOne.X, positionTwo.X);

            for (var j = start; j < end + 1; j++)
            {
                map[new MapNode(j, positionOne.Y)] = NodeType.Rock;
            }
        }
    }
}

var sandEntryPoint = new MapNode(500, 0);

var sandHasOverflowed = false;
while(!sandHasOverflowed)
{
    var currentSandPosition = sandEntryPoint;
    map.Add(sandEntryPoint, NodeType.Sand);

    var itemHasComeToRest = false;
    while (!itemHasComeToRest)
    {
        var verticalDown = new MapNode(currentSandPosition.X, currentSandPosition.Y + 1);
        var diagonalDownLeft = new MapNode(currentSandPosition.X - 1, currentSandPosition.Y + 1);
        var diagonalDownRight = new MapNode(currentSandPosition.X + 1, currentSandPosition.Y + 1);

        if (!map.ContainsKey(verticalDown))
        {
            map.Add(verticalDown, NodeType.Sand);
            map.Remove(currentSandPosition);
            currentSandPosition = verticalDown;
        }
        else if (!map.ContainsKey(diagonalDownLeft))
        {
            map.Add(diagonalDownLeft, NodeType.Sand);
            map.Remove(currentSandPosition);
            currentSandPosition = diagonalDownLeft;
        }
        else if (!map.ContainsKey(diagonalDownRight))
        {
            map.Add(diagonalDownRight, NodeType.Sand);
            map.Remove(currentSandPosition);
            currentSandPosition = diagonalDownRight;
        }
        else
        {
            itemHasComeToRest = true;
        }

        if (currentSandPosition.Y > mapMaximumY)
        {
            sandHasOverflowed = true;
            itemHasComeToRest = true;
            map.Remove(currentSandPosition);
        }
    }
}

for (var y = mapMiniumY - 2; y < mapMaximumY + 2; y++)
{
    for (var x = mapMiniumX - 2; x < mapMaximumX + 2; x++)
    {
        if (map.TryGetValue(new MapNode(x, y), out var nodeType))
        {
            Console.Write(nodeType == NodeType.Rock ? "#" : "o");
        }
        else
        {
            Console.Write(".");
        }
    }
    Console.WriteLine();
}

Console.WriteLine($"Day 14 - Part 1: {map.Count(node => node.Value == NodeType.Sand)}");

record MapNode(int X, int Y);

enum NodeType
{
    Rock,
    Sand
}
