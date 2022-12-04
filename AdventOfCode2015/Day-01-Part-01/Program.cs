var floorInstructions = File.ReadAllText("./input.txt");

var moveUp = floorInstructions.Count(instruction => instruction == '(');
var moveDown = floorInstructions.Count(instruction => instruction == ')');

Console.WriteLine($"Day 1 - Part 1: {moveUp - moveDown}");
