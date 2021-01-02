using System;
using System.Collections.Generic;
using System.IO;

const int numberOfRows = 128;
const int numberOfColumns = 8;

var boardingPassDefinitions = File.ReadAllLines("input.txt");
var boardingPasses = new HashSet<(int row, int column)>(boardingPassDefinitions.Length);
var allPassIds = new HashSet<int>(boardingPassDefinitions.Length);

foreach (var pass in boardingPassDefinitions)
{
    var row = EvaluateBinarySpacePartition(pass.Substring(0, numberOfColumns - 1), 'B', 'F', numberOfRows);
    var column = EvaluateBinarySpacePartition(pass.Substring(numberOfColumns - 2), 'R', 'L', numberOfColumns);
    var id = GetPositionId(row, column);

    boardingPasses.Add((row, column));
    allPassIds.Add(id);
}

var emptySeatId = GetEmptyPostionId(boardingPasses, allPassIds);

Console.WriteLine($"Day 5 - Part 2: {emptySeatId}");

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

int? GetEmptyPostionId(HashSet<(int row, int column)> boardingPasses, HashSet<int> allPassIds)
{
    for (var row = 0; row < numberOfRows; row++) 
    {
        for (var column = 0 ; column < numberOfColumns; column++)
        {
            if (!boardingPasses.Contains((row, column)))
            {
                var candidateId = GetPositionId(row, column);

                if (allPassIds.Contains(candidateId + 1) && allPassIds.Contains(candidateId - 1))
                {
                    return candidateId;
                }
            }
        }
    }

    return null;
}

int GetPositionId(int row, int column) => (row * 8) + column;
