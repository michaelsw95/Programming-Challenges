var instructions = File.ReadAllText("./input.txt");

var santaPosition = (x: 0, y: 0);
var roboSantaPosition = (x: 0, y: 0);

var visted = new HashSet<(int x, int y)>(instructions.Length + 1) { santaPosition };
var moveNext = MoveNext.Santa;

foreach (var instruction in instructions)
{
    if (moveNext == MoveNext.Santa)
    {
        santaPosition = GetNewPosition(instruction, santaPosition);
        visted.Add(santaPosition);
        moveNext = MoveNext.RoboSanta;
    }
    else
    {
        roboSantaPosition = GetNewPosition(instruction, roboSantaPosition);
        visted.Add(roboSantaPosition);
        moveNext = MoveNext.Santa;
    }
}

Console.WriteLine($"Day 3 - Part 2: {visted.Count}");

(int x, int y) GetNewPosition(char instruction, (int x, int y) currentPosition) =>
    instruction switch
    {
        '^' => (currentPosition.x, currentPosition.y + 1),
        'v' => (currentPosition.x, currentPosition.y - 1),
        '<' => (currentPosition.x - 1, currentPosition.y),
        '>' => (currentPosition.x + 1, currentPosition.y)
    };

enum MoveNext
{
    Santa,
    RoboSanta
}
