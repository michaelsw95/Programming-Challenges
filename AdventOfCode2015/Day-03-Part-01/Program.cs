var instructions = File.ReadAllText("./input.txt");

var position = (x: 0, y: 0);
var visted = new HashSet<(int x, int y)>(instructions.Length + 1) { position };

foreach (var instruction in instructions)
{
    position = instruction switch
    {
        '^' => (position.x, position.y + 1),
        'v' => (position.x, position.y - 1),
        '<' => (position.x - 1, position.y),
        '>' => (position.x + 1, position.y)
    };

    visted.Add(position);
}

Console.WriteLine($"Day 3 - Part 1: {visted.Count}");
