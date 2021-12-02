var operations = new Dictionary<string, Func<Position, int, Position>>
{
    { "down", (Position position, int value) => new Position(position.aim + value, position.depth, position.horizontal) },
    { "up", (Position position, int value) => new Position(position.aim - value, position.depth, position.horizontal) },
    { "forward", (Position position, int value) => new Position(position.aim, position.depth + position.aim * value, position.horizontal + value) }
};

var instructions = File.ReadAllLines("./input.txt")
    .Select(line => line.Split(" "))
    .Select(lineParts => new Instruction(lineParts.First(), int.Parse(lineParts.Last())));

var position = new Position(0, 0, 0);

foreach (var instruction in instructions)
{
    position = operations[instruction.direction](position, instruction.value);
}

Console.WriteLine($"Day 2 - Part 2: {position.horizontal * position.depth}");

record Instruction(string direction, int value);
record Position(int aim, int depth, int horizontal);