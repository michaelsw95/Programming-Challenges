var raceInput = File.ReadAllLines("./input.txt")
    .Select(line =>
    {
        var joinedNumber = line.Split(':')
            .Last()
            .Replace(" ", string.Empty);

        return long.Parse(joinedNumber);
    });

var race = new Race(raceInput.First(), raceInput.Last());

var marginOfError = GetNumberOfButtonLengthsToWin(race);

Console.WriteLine($"Day 6 - Part 2: {marginOfError}");

int GetNumberOfButtonLengthsToWin(Race race)
{
    var numberOfButtonLengthsToWin = 0;

    for (var candidateButtonTime = 0; candidateButtonTime < race.Time; candidateButtonTime++)
    {
        var remainingTime = race.Time - candidateButtonTime;

        var newDistancedTravelled = candidateButtonTime * remainingTime;

        if (newDistancedTravelled > race.Distance)
        {
            numberOfButtonLengthsToWin++;
        }
    }

    return numberOfButtonLengthsToWin;
}

record Race(long Time, long Distance);
