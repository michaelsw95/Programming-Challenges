var binaryList = File.ReadAllLines("./input.txt")
    .Select(number => number.ToCharArray());

var lengthOfEachNumber = binaryList.First().Length;
var tally = new Dictionary<int, BinaryTally>();
for (var i = 0; i < lengthOfEachNumber; i++)
{
    tally.Add(i, new BinaryTally(0, 0));
}

foreach (var number in binaryList)
{
    for (var i = 0; i < lengthOfEachNumber; i++)
    {
        if (number[i] == '1')
        {
            tally[i] = tally[i] with { countOfOnes = tally[i].countOfOnes + 1 };
        }
        else
        {
            tally[i] = tally[i] with { countOfZeros = tally[i].countOfZeros + 1 };
        }
    }
}

var gammaRateBinary = string.Empty;
var epsilonRateBinary = string.Empty;

for (var i = 0; i < lengthOfEachNumber; i++)
{
    var currentIsOne = tally[i].countOfOnes > tally[i].countOfZeros;

    gammaRateBinary += currentIsOne ? '1' : '0';
    epsilonRateBinary += currentIsOne ? '0' : '1';
}

var gammaRate = Convert.ToInt32(gammaRateBinary, 2);
var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

Console.WriteLine($"Day 2 - Part 2: {gammaRate * epsilonRate}");

record BinaryTally(int countOfOnes, int countOfZeros);
