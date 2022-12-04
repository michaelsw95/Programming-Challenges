var countOfNiceWords = File.ReadAllLines("./input.txt")
    .Count(IsNiceWord);

Console.WriteLine($"Day 5 - Part 2: {countOfNiceWords}");

bool IsNiceWord(string word) =>
    ContainsTwoLettersThatAppearTwice(word) &&
    ContainsPairWithExactlyOneLetterBetween(word);

bool ContainsTwoLettersThatAppearTwice(string word)
{
    var pairs = new List<(char, char)>();

    for (var i = 0; i < word.Length - 1; i++)
    {
        pairs.Add((word[i], word[i + 1]));
    }

    for (var i = 0; i < pairs.Count; i++)
    {
        var candidatePair = pairs[i];

        for (var j = 0; j < pairs.Count; j++)
        {
            if (candidatePair == pairs[j] && PairIsNotOverlapping(i, j))
            {
                return true;
            }
        }
    }

    return false;

    bool PairIsNotOverlapping(int indexOne, int indexTwo) =>
        indexOne > indexTwo + 1 || indexOne < indexTwo - 1;
}

bool ContainsPairWithExactlyOneLetterBetween(string word)
{
    for (var i = 0; i < word.Length - 2; i++)
    {
        if (word[i] == word[i + 2])
        {
            return true;
        }
    }

    return false;
}
