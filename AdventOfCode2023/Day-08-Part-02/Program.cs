const string startPointEndingChar = "A";
const string endPointEndingChar = "Z";

var mapInput = File.ReadAllLines("./input.txt");

var directions = new Queue<char>(mapInput.First().ToCharArray());

var map = mapInput
    .Skip(2)
    .Select(input => ParseDirection(input, endPointEndingChar))
    .ToDictionary(map => map.Name, direction => direction);

var currentPositions = map
    .Where(position => position.Key.EndsWith(startPointEndingChar))
    .Select(pair => pair.Value)
    .ToArray();

var loopSizes = new Dictionary<Direction, long>(currentPositions.Length);

long moves = 1;

do
{
    var movement = directions.Dequeue();

    for (var i = 0; i < currentPositions.Length; i++)
    {
        currentPositions[i] = movement == 'R'
            ? map[currentPositions[i].RightName]
            : map[currentPositions[i].LeftName];

        if (currentPositions[i].IsTarget)
        {
            loopSizes.TryAdd(currentPositions[i], moves);
        }
    }

    moves++;
    directions.Enqueue(movement);
} while (loopSizes.Count != currentPositions.Length);

var lcm = loopSizes.Values.First();

lcm = loopSizes.Values.Skip(1).Aggregate(lcm, FindLowestCommonMultiple);

Console.WriteLine($"Lowest Common Multiple of Loop Sizes: {lcm}");

Direction ParseDirection(string inputDirection, string targetNodeEndIdentifier)
{
    var directionParts = inputDirection.Split(" = ");
    var leftAndRight = directionParts[1].Substring(1, directionParts[1].Length - 2).Split(", ");
    return new Direction(directionParts[0], leftAndRight[0], leftAndRight[1], directionParts[0].EndsWith(targetNodeEndIdentifier));
}

long FindGreatestCommonDivisor(long a, long b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }
    
    return a;
}

long FindLowestCommonMultiple(long a, long b)
{
    return a / FindGreatestCommonDivisor(a, b) * b;
}

record Direction(string Name, string LeftName, string RightName, bool IsTarget);
