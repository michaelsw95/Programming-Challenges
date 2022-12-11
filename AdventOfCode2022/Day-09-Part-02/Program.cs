var moveInstructions = File
    .ReadAllLines("input.txt")
    .Select(instruction => {
        var instructionParts = instruction.Split(' ');

        var direction = instructionParts[0] switch
        {
            "R" => Direction.Right,
            "L" => Direction.Left,
            "U" => Direction.Up,
            "D" => Direction.Down,
            _ => throw new Exception("Invalid direction")
        };

        var distance = int.Parse(instructionParts[1]);
        return new Instruction(direction, distance);
    })
    .ToArray();

const int ChainLength = 10;
var chain = new Position[ChainLength];
for (var i = 0; i < ChainLength; i++)
{
    chain[i] = new Position(0, 0);
}

var vistedTailPositions = new HashSet<Position>() { chain.Last() };

foreach (var move in moveInstructions)
{
    var iterateFunc = GetIteratorForMove(move.Direction);

    for (var i = 0; i < move.Distance; i++)
    {
        chain[0] = iterateFunc(chain[0]);

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

Func<Position, Position> GetIteratorForMove(Direction move) => move switch
{
    Direction.Right => IterateRight,
    Direction.Left => IterateLeft,
    Direction.Up => IterateUp,
    Direction.Down => IterateDown,
    _ => throw new Exception("Invalid direction")
};

Position IterateRight(Position Position) => new Position(Position.X + 1, Position.Y);
Position IterateLeft(Position Position) => new Position(Position.X - 1, Position.Y);
Position IterateUp(Position Position) => new Position(Position.X, Position.Y + 1);
Position IterateDown(Position Position) => new Position(Position.X, Position.Y - 1);

record Position(int X, int Y);
record Instruction(Direction Direction, int Distance);

enum Direction
{
    Right,
    Left,
    Up,
    Down
}
