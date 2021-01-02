using System;
using System.IO;
using System.Linq;

var sumOfAnswers = File
    .ReadAllText("input.txt")
    .Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
    .Select(CountDistinctAnswers)
    .Sum();

Console.WriteLine($"Day 6 - Part 1: {sumOfAnswers}");

int CountDistinctAnswers(string answer) => answer.ToCharArray()
    .Where(answer => !Char.IsWhiteSpace(answer))
    .GroupBy(answer => answer)
    .Count();
