using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

const string targetBag = "shiny gold";

var ruleDefinitions = File.ReadAllLines("input.txt").ToArray();
var rules = ruleDefinitions
    .Select(rule => rule.Split(" bags contain "))
    .ToDictionary(splitRule => splitRule.First(), splitRule => GetChildrenFromRule(splitRule.Last()));

var countOfChildren = GetCountOfChildren(targetBag, rules);

Console.WriteLine($"Day 7 - Part 2: {countOfChildren}");

List<(string, int)> GetChildrenFromRule(string rule)
{
    if (rule == "no other bags.")
    {
        return new List<(string, int)>(0);
    }

    var countOfChildren = Regex.Matches(rule, "[0-9]+");
    var children = Regex.Matches(rule, @"\w+ \w+ (bags|bag)");
    var childrenNames = new List<(string, int)>(children.Count);

    for (var i = 0; i < children.Count; i++)
    {
        var child = children.ElementAt(i).ToString();
        var childCount = int.Parse(countOfChildren.ElementAt(i).ToString());

        var childName = child.ToString()
            .Replace("bags", string.Empty)
            .Replace("bag", string.Empty)
            .Trim();
        
        childrenNames.Add((childName, childCount));
    }

    return childrenNames;
}

int GetCountOfChildren(string targetBag, Dictionary<string, List<(string Name, int Count)>> rules) =>
    rules[targetBag]
        .Sum(child => child.Count + (child.Count * GetCountOfChildren(child.Name, rules)));
