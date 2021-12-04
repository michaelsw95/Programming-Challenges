var binaryList = File.ReadAllLines("./input.txt")
    .Select(number => number.ToCharArray())
    .ToArray();

var oxygenRatingBinary = GetRating(binaryList, BitCriteria.MostCommon);
var carbonDioxideRatingBinary = GetRating(binaryList, BitCriteria.LeastCommon);

var oxygenRating = Convert.ToInt32(oxygenRatingBinary, 2);
var carbonDioxideRating = Convert.ToInt32(carbonDioxideRatingBinary, 2);

Console.WriteLine($"Day 2 - Part 2: {oxygenRating * carbonDioxideRating}");

string GetRating(char[][] binaryList, BitCriteria criteria)
{
    var lengthOfEachNumber = binaryList.First().Length;
    var candidateIndexes = Enumerable.Range(0, binaryList.Length).ToHashSet();

    for (var i = 0; i < lengthOfEachNumber; i++)
    {
        var countOfOnes = 0;
        var countOfZeros = 0;
        var indexPositionOfOnes = new HashSet<int>();

        for (var j = 0; j < binaryList.Length; j++)
        {
            if (!candidateIndexes.Contains(j))
            {
                continue;
            }

            if (binaryList[j][i] == '1')
            {
                countOfOnes++;
                indexPositionOfOnes.Add(j);
            }
            else
            {
                countOfZeros++;
            }
        }

        var compareFunc = criteria == BitCriteria.MostCommon ? 
            new Func<int, int, bool>((countOfOnes, countOfZeros) => countOfOnes >= countOfZeros) :
            new Func<int, int, bool>((countOfOnes, countOfZeros) => countOfOnes < countOfZeros);

        candidateIndexes = GetFilteredCandidateIndexes(
            compareFunc, candidateIndexes, indexPositionOfOnes, countOfOnes, countOfZeros);

        if (candidateIndexes.Count == 1)
        {
            break;
        }
    }

    return new string(binaryList[candidateIndexes.Single()]);
}

HashSet<int> GetFilteredCandidateIndexes(
    Func<int, int, bool> bitCriteriaCompare,
    HashSet<int> candidateIndexes,
    HashSet<int> indexPositionOfOnes,
    int countOfOnes,
    int countOfZeros)
{
    var filteredIndexes = new HashSet<int>(candidateIndexes);

    if (bitCriteriaCompare(countOfOnes, countOfZeros))
    {
        filteredIndexes.RemoveWhere(index => !indexPositionOfOnes.Contains(index));
    }
    else
    {
        filteredIndexes.RemoveWhere(index => indexPositionOfOnes.Contains(index));
    }

    return filteredIndexes;
}

enum BitCriteria
{
    MostCommon,
    LeastCommon
}
