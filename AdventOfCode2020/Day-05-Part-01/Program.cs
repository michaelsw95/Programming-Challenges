using System;
using System.IO;
using System.Linq;

var highestBoaringPassId = File.ReadAllLines("input.txt")
    .Select(pass => GetPositionId(new Position
    {
        Row = EvaluateBinarySpacePartition(pass.Substring(0, 7), 'B', 'F', 128),
        Seat = EvaluateBinarySpacePartition(pass.Substring(6), 'R', 'L', 8)
    }))
    .Max();

Console.WriteLine($"Day 5 - Part 1: {highestBoaringPassId}");

int EvaluateBinarySpacePartition(string positionString, char upperId, char lowerId, int partitionUpperBound)
{
    (int lowBound, int upperBound) rangeInConsideration = (0, partitionUpperBound - 1);
    foreach (var direction in positionString)
    {
        var midPoint = (float)(rangeInConsideration.lowBound + rangeInConsideration.upperBound) / 2;

        if (direction == lowerId)
        {
            rangeInConsideration.upperBound = (int)Math.Floor(midPoint);
        }
        else if (direction == upperId)
        {
            rangeInConsideration.lowBound = (int)Math.Ceiling(midPoint);
        }
    }

    return rangeInConsideration.lowBound;
}

int GetPositionId(Position position) => (position.Row * 8) + position.Seat;

record Position
{
    public int Row { get; init; }
    public int Seat { get; init; }
}
