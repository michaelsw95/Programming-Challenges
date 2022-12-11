var chain = new Position[10];

for (var i = 0; i < chain.Length; i++)
{
    chain[i] = new Position(0, 0);
}

var vistedTailPositions = new HashSet<Position>() { chain.Last() };

var instructions = File.ReadAllLines("input.txt");
foreach (var move in instructions)
{
    var instructionParts = move.Split(' ');

    Func<Position, Position> iterateForDirection = instructionParts[0] switch
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
        chain[0] = iterateForDirection(chain[0]);

        for (var j = 0; j < chain.Length - 1; j++)
        {
            if (!ArePositionsTouching(chain[j], chain[j + 1]))
            {
                var closestFlankToCurrent = GetClosestFlankBetweenPostions(chain[j], chain[j + 1]);
                chain[j + 1] = closestFlankToCurrent;
            }   
        }

        vistedTailPositions.Add(chain.Last());
    }
}

Console.WriteLine($"Day 9 - Part 2: {vistedTailPositions.Count}");

bool ArePositionsTouching(Position positionA, Position positionB)
{
    var xDistance = Math.Abs(positionA.X - positionB.X);
    var yDistance = Math.Abs(positionA.Y - positionB.Y);

    return xDistance <= 1 && yDistance <= 1;
}

Position GetClosestFlankBetweenPostions(Position positionA, Position positionB)
{
    Position[] GetPositionsSortedByDistanceToTarget(List<Position> allPositions, Position firstPosition, Position secondPosition) => allPositions
        .Select(position => (Position: position, Difference: GetPositionDifference(position, positionB)))
        .Where(positionCalculation => positionCalculation.Difference.XDifference <= 1 && positionCalculation.Difference.YDifference <= 1)
        .OrderBy(positionCalculation => positionCalculation.Difference.XDifference + positionCalculation.Difference.YDifference)
        .Select(positionCalculation => positionCalculation.Position)
        .ToArray();

    var normalNeighbourPositions = new List<Position>
    {
        new Position(positionA.X, positionA.Y + 1),
        new Position(positionA.X, positionA.Y - 1),

        new Position(positionA.X + 1, positionA.Y),
        new Position(positionA.X - 1, positionA.Y),
    };

    var diagonalNeighbourPositions = new List<Position>
    {
        new Position(positionA.X + 1, positionA.Y + 1),
        new Position(positionA.X - 1, positionA.Y + 1),

        new Position(positionA.X + 1, positionA.Y - 1),
        new Position(positionA.X - 1, positionA.Y - 1),
    };
   
    var neighboursByDistance = GetPositionsSortedByDistanceToTarget(normalNeighbourPositions, positionA, positionB);

    return neighboursByDistance.Any() ?
        neighboursByDistance.First() :
        GetPositionsSortedByDistanceToTarget(diagonalNeighbourPositions, positionA, positionB).First();
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
