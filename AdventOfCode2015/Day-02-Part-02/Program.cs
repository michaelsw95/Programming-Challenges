var feetOfRibbon = File.ReadAllLines("./input.txt")
    .Select(ParsePresentDimensions)
    .Select(GetRequiredLengthOfRibbon)
    .Sum();

Console.WriteLine($"Day 2 - Part 2: {feetOfRibbon}");

PresentDimensions ParsePresentDimensions(string line)
{
    var numbers = line.Split("x").Select(rawNumber => int.Parse(rawNumber)).ToArray();

    return new PresentDimensions(numbers[0], numbers[1], numbers[2]);
}

int GetRequiredLengthOfRibbon(PresentDimensions present)
{
    var presentDimensionsOrdered = GetPresentDimensionsOrdered(present);

    var presentRibbonLength = (2 * presentDimensionsOrdered[0]) + (2 * presentDimensionsOrdered[1]);

    var bowRibbonLength = present.length * present.height * present.width;

    return presentRibbonLength + bowRibbonLength;
}

int[] GetPresentDimensionsOrdered(PresentDimensions present) => 
    new int[3] { present.length, present.height, present.width }
    .OrderBy(dimension => dimension)
    .ToArray();

record PresentDimensions(int length, int width, int height);
