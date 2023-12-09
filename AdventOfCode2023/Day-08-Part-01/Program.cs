const string startPoint = "AAA";
const string endPoint = "ZZZ";

var mapInput = File.ReadAllLines("./input.txt");

var directions = new Queue<char>(mapInput.First().ToCharArray());

var map = mapInput
    .Skip(2)
    .Select(ParseDirection)
    .ToDictionary(map => map.Name, direction => direction);

var currentPosition = map[startPoint];
var moves = 0;

do
{
    var movement = directions.Dequeue();

    currentPosition = movement == 'R' ?
        map[currentPosition.RightName] : map[currentPosition.LeftName];

    moves++;
    
    directions.Enqueue(movement);
} while (currentPosition.Name != endPoint);

Console.WriteLine($"Day 8 - Part 1: {moves}");

Direction ParseDirection(string inputDirection)
{
    var directionParts = inputDirection.Split(" = ");

    var leftAndRight = directionParts[1].Substring(1, directionParts[1].Length - 2).Split(", ");

    return new Direction(directionParts[0], leftAndRight[0], leftAndRight[1]);
}

record Direction (string Name, string LeftName, string RightName);
