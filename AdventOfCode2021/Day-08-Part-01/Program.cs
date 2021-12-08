var rawSignalEntries = File.ReadAllLines("./input.txt");

var uniqueSegmentCounts = new HashSet<int>() { 2, 4, 3, 7 };

var countOfIndentifiedNumbers = 0;
foreach (var line in rawSignalEntries)
{
    countOfIndentifiedNumbers += line
        .Split('|')
        .Last()
        .Split(" ")
        .Where(linePart => !string.IsNullOrWhiteSpace(linePart))
        .Where(linePart => uniqueSegmentCounts.Contains(linePart.Length))
        .Count();
}

Console.WriteLine($"Day 8 - Part 1: {countOfIndentifiedNumbers}");
