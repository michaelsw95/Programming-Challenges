var sumOfAllPoints = File.ReadAllLines("./input.txt")
    .Select(GetPointsFromScoreCard)
    .Sum();
    
Console.WriteLine($"Day 4 - Part 1: {sumOfAllPoints}");

int GetPointsFromScoreCard(string scoreCardLine)
{
    var scores = scoreCardLine.Split(":")[1].Trim();

    var scoreParts = scores.Split("|");

    var winningNumbers = GetNumbersFromScoreStrings(scoreParts[0]).ToHashSet();
    
    var numbersWeHave = GetNumbersFromScoreStrings(scoreParts[1]);

    var matches = numbersWeHave.Count(number => winningNumbers.Contains(number));

    var points = matches > 0 ? 1 : 0;
    for (var i = 1; i < matches; i++)
    {
        points *= 2;
    }

    return points;
}

IEnumerable<int> GetNumbersFromScoreStrings(string scoreString) =>
    scoreString
        .Split(" ")
        .Where(score => !string.IsNullOrEmpty(score))
        .Select(int.Parse);
