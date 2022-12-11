var headPosition = new Position(0, 0);
var tailPosition = new Position(0, 0);
var vistedTailPositions = new HashSet<Position>() { tailPosition };

var instructions = File.ReadAllLines("input.txt");

foreach (var move in instructions)
{
    var instructionParts = move.Split(' ');

    Func<Position, Position> iteratorForDirection = instructionParts[0] switch
    {
        "R" => IterateRight,
        "L" => IterateLeft,
        "U" => IterateUp,
        "D" => IterateDown,
        _ => throw new Exception("Invalid direction")
    };

    var distance = int.Parse(instructionParts[1]);

    for (var i = 0; i < distance; i++)
    {
        headPosition = iteratorForDirection(headPosition);
    
        if (!ArePositionsTouching(headPosition, tailPosition))
        {
            var closestFlankToHead = GetClosestFlankBetweenPostions(headPosition, tailPosition);
            tailPosition = closestFlankToHead;
            vistedTailPositions.Add(tailPosition);
        }
    }
}

Console.WriteLine($"Day 9 - Part 1: {vistedTailPositions.Count}");

bool ArePositionsTouching(Position positionA, Position positionB)
{
    var xDistance = Math.Abs(positionA.X - positionB.X);
    var yDistance = Math.Abs(positionA.Y - positionB.Y);

    return xDistance <= 1 && yDistance <= 1;
}

Position GetClosestFlankBetweenPostions(Position positionA, Position positionB)
{
    var neighborPositions = new List<Position>
    {
        new Position(positionA.X, positionA.Y + 1),
        new Position(positionA.X, positionA.Y - 1),
        new Position(positionA.X + 1, positionA.Y),
        new Position(positionA.X - 1, positionA.Y)
    };
   
    var neighboursByDistance = neighborPositions
        .Select(position => (Position: position, Difference: GetPositionDifference(position, positionB)))
        .Where(positionCalculation => positionCalculation.Difference.XDifference <= 1 && positionCalculation.Difference.YDifference <= 1)
        .OrderBy(positionCalculation => positionCalculation.Difference.XDifference + positionCalculation.Difference.YDifference)
        .Select(positionCalculation => positionCalculation.Position);

    return neighboursByDistance.First();
}

(int XDifference, int YDifference) GetPositionDifference(Position positionA, Position positionB)
{
    var xDistance = Math.Abs(positionA.X - positionB.X);
    var yDistance = Math.Abs(positionA.Y - positionB.Y);

    return (xDistance, yDistance);
}

Position IterateRight(Position Position) => new Position(Position.X + 1, Position.Y);
Position IterateLeft(Position Position) => new Position(Position.X - 1, Position.Y);
Position IterateUp(Position Position) => new Position(Position.X, Position.Y + 1);
Position IterateDown(Position Position) => new Position(Position.X, Position.Y - 1);

record Position(int X, int Y);
