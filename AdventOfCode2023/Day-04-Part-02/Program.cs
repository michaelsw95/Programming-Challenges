var scratchCardInput = File.ReadAllLines("./input.txt");

var totalNumberOfScratchCardCopies = scratchCardInput.Length;
for (var i = 0; i < scratchCardInput.Length; i++)
{
    totalNumberOfScratchCardCopies += GetNumberOfCopies(scratchCardInput, i);
}
    
Console.WriteLine($"Day 4 - Part 2: {totalNumberOfScratchCardCopies}");

int GetNumberOfCopies(string[] scratchCards, int startingCardIndex)
{
    if (startingCardIndex >= scratchCards.Length)
    {
        return 0;
    }
    
    var matches = GetMatchesFromScoreCard(scratchCards[startingCardIndex]);

    var copiesWon = matches;

    for (var i = 1; i < matches + 1; i++)
    {
        copiesWon += GetNumberOfCopies(scratchCards, i + startingCardIndex);
    }

    return copiesWon;
}

int GetMatchesFromScoreCard(string scoreCardLine)
{
    var scores = scoreCardLine.Split(":")[1].Trim();

    var scoreParts = scores.Split("|");

    var winningNumbers = GetNumbersFromScoreStrings(scoreParts[0]).ToHashSet();
    
    var numbersWeHave = GetNumbersFromScoreStrings(scoreParts[1]);

    return numbersWeHave.Count(number => winningNumbers.Contains(number));
}

IEnumerable<int> GetNumbersFromScoreStrings(string scoreString) =>
    scoreString
        .Split(" ")
        .Where(score => !string.IsNullOrEmpty(score))
        .Select(int.Parse);
        