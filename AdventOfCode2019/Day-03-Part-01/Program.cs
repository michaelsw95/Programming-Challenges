using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_03_Part_01
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

            var crossoverPoints = GetCrossoverPoints(lineOneVisited, lineTwoVisited);
            var crossoverDistances = new int[crossoverPoints.Count];

            for (var i = 0; i < crossoverPoints.Count; i++)
            {
                var manhattanX = GetDistanceBetweenPoints(origin.xPos, crossoverPoints[i].xPos);
                var manhattanY = GetDistanceBetweenPoints(origin.yPos, crossoverPoints[i].yPos);

                crossoverDistances[i] = manhattanX + manhattanY;
            }

            Console.WriteLine("Day 3 - Part 1: " + crossoverDistances.Min());            
        }

        static HashSet<(int xPos, int yPos)> GetVisitedPath(
            (int xPos, int yPos) origin,
            string[] moveInstructions)
        {
            var position = (xPos: origin.xPos, yPos: origin.yPos);
            
            var visited = new HashSet<(int xPos, int yPos)>();

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

        static List<(int xPos, int yPos)> GetCrossoverPoints(
            HashSet<(int xPos, int yPos)> firstPath,
            HashSet<(int xPos, int yPos)> secondPath)
        {
            var crossoverPoints = new List<(int xPos, int yPos)>();

            foreach (var point in firstPath)
            {
                if (secondPath.Contains(point))
                {
                    crossoverPoints.Add(point);
                }
            }

            return crossoverPoints;
        }

        static int GetDistanceBetweenPoints(int origin, int newPoint) =>
            newPoint > origin ? newPoint - origin : origin - newPoint;
    }
}
