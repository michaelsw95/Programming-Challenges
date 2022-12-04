var countOfNiceWords = File.ReadAllLines("./input.txt")
    .Count(IsNiceWord);

Console.WriteLine($"Day 5 - Part 1: {countOfNiceWords}");

bool IsNiceWord(string word) =>
    ContainsThreeVowels(word) &&
    ContainsConsecutiveLetters(word) &&
    !ContainsBannedStrings(word);

bool ContainsThreeVowels(string word)
{
    var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

    var countOfVowels = 0;
    foreach (var letter in word)
    {
        if (vowels.Contains(letter))
        {
            countOfVowels++;

            if (countOfVowels >= 3)
            {
                return true;
            }
        }
    }

    return false;
}

bool ContainsConsecutiveLetters(string word)
{
    var lastLetter = default(char);

    foreach (var letter in word)
    {
        if (lastLetter == letter)
        {
            return true;
        }

        lastLetter = letter;
    }

    return false;
}

bool ContainsBannedStrings(string word)
{
    return new string[] { "ab", "cd", "pq", "xy" }.Any(bannedWord => word.Contains(bannedWord));
}
