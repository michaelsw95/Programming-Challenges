using System;
using System.IO;
using System.Linq;

var validPasswords = GetValidPasswords("./input.txt");

Console.WriteLine($"Day 2 - Part 1: {validPasswords.Length}");

Password[] GetValidPasswords(string filePath)
{
    Password ParsePassword(string password)
    {
        var halfs = password.Split(": ");

        var character = halfs[0].Last();
        var requiredNumberSubString = halfs[0]
            .Substring(0, halfs[0].Length - 2)
            .Split("-");

        var lowerBound = int.Parse(requiredNumberSubString[0]);
        var upperBound = int.Parse(requiredNumberSubString[1]);

        return new Password(character, halfs[1], lowerBound, upperBound);
    }

    bool IsValidPassword(Password password)
    {
        var countOfRequiredLetter = password.PasswordText
            .Where(o => o == password.LetterRequired)
            .Count();

        return password.RequiredCount.lower <= countOfRequiredLetter
            && password.RequiredCount.upper >= countOfRequiredLetter;
    }

    return File.ReadAllLines(filePath)
        .Select(ParsePassword)
        .Where(IsValidPassword)
        .ToArray();
}
    
record Password
{
    public Password(char letterRequired, string password, int lowerCount, int upperCount)
    {
        LetterRequired = letterRequired;
        RequiredCount = (lowerCount, upperCount);
        PasswordText = password;
    }

    public char LetterRequired { get; init; }
    public (int lower, int upper) RequiredCount { get; init; }
    public string PasswordText { get; init; }
}
