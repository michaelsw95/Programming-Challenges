var squareFeetOfWrappingPaper = File.ReadAllLines("./input.txt")
    .Select(ParsePresentDimensions)
    .Select(GetSurfaceAreaOfPresent)
    .Sum();

Console.WriteLine($"Day 2 - Part 1: {squareFeetOfWrappingPaper}");

PresentDimensions ParsePresentDimensions(string line)
{
    var numbers = line.Split("x").Select(rawNumber => int.Parse(rawNumber)).ToArray();

    return new PresentDimensions(numbers[0], numbers[1], numbers[2]);
}

int GetSurfaceAreaOfPresent(PresentDimensions present)
{
    var sideOne = present.length * present.width;
    var sideTwo = present.width * present.height;
    var sideThree = present.height * present.length;

    var smallestSide = GetSmallestNumber(sideOne, sideTwo, sideThree);

    return (2 * sideOne) + (2 * sideTwo) + (2 * sideThree) + smallestSide;
}

int GetSmallestNumber(params int[] numbers) => numbers.Min();

record PresentDimensions(int length, int width, int height);
