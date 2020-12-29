using System;
using System.IO;

var map = File.ReadAllLines("input.txt");

long treeCount = GetCountOfStepEncounters((1, 1), map, '#');
treeCount *= GetCountOfStepEncounters((1, 3), map, '#');
treeCount *= GetCountOfStepEncounters((1, 5), map, '#');
treeCount *= GetCountOfStepEncounters((1, 7), map, '#');
treeCount *= GetCountOfStepEncounters((2, 1), map, '#');

Console.WriteLine($"Day 3 - Part 2: {treeCount}");

int GetCountOfStepEncounters((int x, int y) step, string[] map, char target)
{
    (int x, int y) currentPosition = (0, 0);
    var targetCount = 0;

    while (currentPosition.x < map.Length)
    {
        if (map[currentPosition.x][currentPosition.y % map[0].Length] == target)
        {
            targetCount += 1;
        }

        currentPosition.x += step.x;
        currentPosition.y += step.y;
    }

    return targetCount;
}