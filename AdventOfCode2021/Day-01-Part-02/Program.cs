var sonarSweaps = File.ReadAllLines("./input.txt")
    .Select(line => int.Parse(line))
    .ToArray();

var countOfIncreasingGroupDepths = 0;

for (var i = 0; i < sonarSweaps.Length - 3; i++)
{
    var currentGroupSum = sonarSweaps[i] + sonarSweaps[i + 1] + sonarSweaps[i + 2];
    var nextGroupSum = sonarSweaps[i + 1] + sonarSweaps[i + 2] + sonarSweaps[i + 3];

    if (currentGroupSum < nextGroupSum)
    {
        countOfIncreasingGroupDepths++;
    }
}

Console.WriteLine($"Day 1 - Part 2: {countOfIncreasingGroupDepths}");
