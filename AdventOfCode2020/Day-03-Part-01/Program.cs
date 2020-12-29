using System;
using System.IO;

var map = File.ReadAllLines("input.txt");

const char tree = '#';
var treeCount = 0;
(int x, int y) step = (1, 3);
(int x, int y) currentPosition = (0, 0);

while (currentPosition.x < map.Length)
{
    if (map[currentPosition.x][currentPosition.y % map[0].Length] == tree)
    {
        treeCount += 1;
    }

    currentPosition.x += step.x;
    currentPosition.y += step.y;
}

Console.WriteLine($"Day 3 - Part 1: {treeCount}");