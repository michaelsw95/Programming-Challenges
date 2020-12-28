using System;
using System.IO;
using System.Linq;

var validPasswords = GetValidPasswords("./input.txt");

Console.WriteLine($"Day 2 - Part 2: {validPasswords.Length}");

Password[] GetValidPasswords(string filePath)
{
    Password ParsePassword(string password)
    {
        var halfs = password.Split(": ");

        var character = halfs[0].Last();
        var requiredIndexesSubString = halfs[0]
            .Substring(0, halfs[0].Length - 2)
            .Split("-");

        var lowerBound = int.Parse(requiredIndexesSubString[0]);
        var upperBound = int.Parse(requiredIndexesSubString[1]);

        return new Password(character, halfs[1], lowerBound - 1, upperBound - 1);
    }

    bool IsValidPassword(Password password)
    {
        var countOfRequiredLetter = password.PasswordText
            .Where(o => o == password.LetterRequired)
            .Count();

        return
            (password.PasswordText[password.RequiredIndexes.lower] == password.LetterRequired &&
            password.PasswordText[password.RequiredIndexes.upper] != password.LetterRequired) || 
            (password.PasswordText[password.RequiredIndexes.upper] == password.LetterRequired &&
            password.PasswordText[password.RequiredIndexes.lower] != password.LetterRequired);
    }

    return File.ReadAllLines(filePath)
        .Select(ParsePassword)
        .Where(IsValidPassword)
        .ToArray();
}
    
record Password
{
    public Password(char letterRequired, string password, int lower, int upper)
    {
        LetterRequired = letterRequired;
        RequiredIndexes = (lower, upper);
        PasswordText = password;
    }

    public char LetterRequired { get; init; }
    public (int lower, int upper) RequiredIndexes { get; init; }
    public string PasswordText { get; init; }
}
