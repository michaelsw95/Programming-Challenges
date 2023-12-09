var raceInput = File.ReadAllLines("./input.txt")
    .Select(line =>
        line.Split(':')
            .Last()
            .Split(' ')
            .Where(raceValue => !string.IsNullOrWhiteSpace(raceValue))
            .Select(int.Parse)
        );

var marginOfError = raceInput
    .First()
    .Zip(raceInput.Last(), (time, distance) => new Race(time, distance))
    .Select(GetNumberOfButtonLengthsToWin)
    .Aggregate(1, (raceOutputOne, raceOutputTwo) => raceOutputOne * raceOutputTwo);

Console.WriteLine($"Day 6 - Part 1: {marginOfError}");

int GetNumberOfButtonLengthsToWin(Race race)
{
    var candidateHoldButtonTimes = Enumerable.Range(0, race.Time);
    var numberOfButtonLengthsToWin = 0;

    foreach (var buttonTime in candidateHoldButtonTimes)
    {
        var remainingTime = race.Time - buttonTime;

        var newDistancedTravelled = buttonTime * remainingTime;

        if (newDistancedTravelled > race.Distance)
        {
            numberOfButtonLengthsToWin++;
        }
    }

    return numberOfButtonLengthsToWin;
}

record Race(int Time, int Distance);
