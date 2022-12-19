var trees = File.ReadAllLines("./input.txt")
    .Select(rowOfTrees => rowOfTrees.ToCharArray().Select(tree => int.Parse(tree.ToString())).ToArray())
    .ToArray();

var countOfVisibleTrees = (trees.Length * 2) + (trees[0].Length * 2) - 4;
var currentPosition = new Position(1, 1);

for (var i = 1; i < trees.Length - 1; i++)
{
    for (var j = 1; j < trees[0].Length - 1; j++)
    {
        currentPosition = new Position(i, j);
        if (IsVisible(FromTheTop) || IsVisible(FromTheBottom)|| IsVisible(FromTheRight) || IsVisible(FromTheLeft))
        {
            countOfVisibleTrees++;
        }
    }
}

Console.WriteLine($"Day 8 - Part 1: {countOfVisibleTrees}");

bool IsVisible(Func<Position, Position> iterate)
{
    var position = iterate(currentPosition);

    while (IsValidTreePosition(trees, position))
    {
        if (trees[position.X][position.Y] >= trees[currentPosition.X][currentPosition.Y])
        {
            return false;
        }

        position = iterate(position);
    }

    return true;
}

bool IsValidTreePosition(int[][] trees, Position position) => 
    position.X >= 0 && position.X <= trees.Length - 1 && position.Y >= 0 && position.Y <= trees.Length - 1;

Position FromTheTop(Position currentPosition) => currentPosition with { Y = currentPosition.Y - 1 };
Position FromTheBottom(Position currentPosition) => currentPosition with { Y = currentPosition.Y + 1 };
Position FromTheLeft(Position currentPosition) => currentPosition with { X = currentPosition.X - 1 };
Position FromTheRight(Position currentPosition) => currentPosition with { X = currentPosition.X + 1 };

record Position(int X, int Y);
