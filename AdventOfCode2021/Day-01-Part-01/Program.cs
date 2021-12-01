var sonarSweaps = File.ReadAllLines("./input.txt")
    .Select(line => int.Parse(line))
    .ToArray();

var countOfIncreasingDepths = Enumerable.Range(1, sonarSweaps.Length - 1)
    .Count(i => sonarSweaps[i] > sonarSweaps[i - 1]);

Console.WriteLine($"Day 1 - Part 1: {countOfIncreasingDepths}");
