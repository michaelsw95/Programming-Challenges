using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_03_Part_02
{
    public enum MapDirection: int
    {
        R,
        L,
        U,
        D
    }

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines(args[0])
                .Select(o => o.Split(','))
                .ToArray();
            
            var lineOne = input[0];
            var lineTwo = input[1];

            (int xPos, int yPos) origin = (0, 0);

            var lineOneVisited = GetVisitedPath(origin, lineOne);
            var lineTwoVisited = GetVisitedPath(origin, lineTwo);

            var minDistanceToCrossover = GetCrossoverPoints(lineOneVisited, lineTwoVisited)
                .Select(o => o.totalPathLength)
                .Min();

            Console.WriteLine("Day 3 - Part 2: " + minDistanceToCrossover);            
        }

        static List<(int xPos, int yPos)> GetVisitedPath(
            (int xPos, int yPos) origin,
            string[] moveInstructions)
        {
            var position = (xPos: origin.xPos, yPos: origin.yPos);
            
            var visited = new List<(int xPos, int yPos)>();

            foreach (var instruction in moveInstructions)
            {
                var moveDirection = instruction.First().ToString();
                var moveAmount = Convert.ToInt32(instruction.Substring(1));

                if (!Enum.TryParse(moveDirection, out MapDirection direction))
                {
                    throw new InvalidDataException();
                }

                for (var j = 0; j < moveAmount; j++)
                {
                    position = CalculateNewPosition(direction, position);

                    visited.Add(position);
                }
            }

            return visited;
        }

        static (int xPos, int yPos) CalculateNewPosition
            (MapDirection direction, (int xPos, int yPos) position) 
        {
            var newPosition = position;

            switch (direction)
            {
                case MapDirection.R:
                    newPosition.xPos += 1;
                    break;
                case MapDirection.L:
                    newPosition.xPos -= 1;
                    break;
                case MapDirection.U:
                    newPosition.yPos += 1;
                    break;
                case MapDirection.D:
                    newPosition.yPos -= 1;
                    break;
            }

            return newPosition;
        }

        static List<(int xPos, int yPos, int totalPathLength)> GetCrossoverPoints(
            List<(int xPos, int yPos)> firstPath,
            List<(int xPos, int yPos)> secondPath)
        {
            var crossoverPoints = new List<(int xPos, int yPos, int totalPathLength)>();

            var secondPathVisitedLookup = new HashSet<(int xPos, int yPos)>(secondPath);

            for (var i = 0; i < firstPath.Count; i++)
            {
                if (secondPathVisitedLookup.Contains((firstPath[i])))
                {
                    for (var j = 0; j < secondPath.Count; j++)
                    {
                        if (firstPath[i].xPos == secondPath[j].xPos
                            && firstPath[i].yPos == secondPath[j].yPos)
                        {
                            var stepsRequired = i + j + 2;
                            crossoverPoints.Add((firstPath[i].xPos, firstPath[i].yPos, stepsRequired));
                        }
                    }
                }
            }

            return crossoverPoints;
        }

        static int GetDistanceBetweenPoints(int origin, int newPoint) =>
            newPoint > origin ? newPoint - origin : origin - newPoint;
    }
}
