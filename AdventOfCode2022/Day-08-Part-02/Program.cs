var trees = File.ReadAllLines("./input.txt")
    .Select(rowOfTrees => rowOfTrees.ToCharArray().Select(tree => int.Parse(tree.ToString())).ToArray())
    .ToArray();

var currentPosition = new Position(1, 1);
var highestScenicScore = 0;

for (var i = 1; i < trees.Length - 1; i++)
{
    for (var j = 1; j < trees[0].Length - 1; j++)
    {
        currentPosition = new Position(i, j);
        var up = GetCountOfVisibleTrees(Upwards);
        var down = GetCountOfVisibleTrees(Downwards);
        var left = GetCountOfVisibleTrees(Leftwards);
        var right = GetCountOfVisibleTrees(Rightwards);

        highestScenicScore = Math.Max(highestScenicScore, up * down * left * right);
    }
}

Console.WriteLine($"Day 8 - Part 2: {highestScenicScore}");

int GetCountOfVisibleTrees(Func<Position, Position> iterate)
{
    var position = iterate(currentPosition);

    var countOfVisibleTrees = 0;

    while (IsValidTreePosition(trees, position))
    {
        if (trees[position.X][position.Y] >= trees[currentPosition.X][currentPosition.Y])
        {
            return countOfVisibleTrees + 1;
        }

        countOfVisibleTrees++;
        position = iterate(position);
    }

    return Math.Max(1, countOfVisibleTrees);
}

bool IsValidTreePosition(int[][] trees, Position position) => 
    position.X >= 0 && position.X <= trees.Length - 1 && position.Y >= 0 && position.Y <= trees.Length - 1;

Position Upwards(Position currentPosition) => currentPosition with { Y = currentPosition.Y - 1 };
Position Downwards(Position currentPosition) => currentPosition with { Y = currentPosition.Y + 1 };
Position Leftwards(Position currentPosition) => currentPosition with { X = currentPosition.X - 1 };
Position Rightwards(Position currentPosition) => currentPosition with { X = currentPosition.X + 1 };

record Position(int X, int Y);
