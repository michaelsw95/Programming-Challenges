var instructions = File.ReadAllLines("./input.txt")
    .Select(line => line.Split(" "))
    .GroupBy(lineParts => lineParts.First(), lineParts => int.Parse(lineParts.Last()))
    .ToDictionary(group => group.Key, group => group.Sum());

var depth = instructions["down"] - instructions["up"];
var forward = instructions["forward"];

Console.WriteLine($"Day 2 - Part 1: {depth * forward}");
