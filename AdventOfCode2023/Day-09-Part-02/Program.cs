var sensorReport = File
    .ReadAllLines("./input.txt")
    .Select(line => line.Split(' ').Select(int.Parse).ToList());

var extrapolatedValues = new List<int>();

foreach (var line in sensorReport)
{
    var history = new List<List<int>> { line };

    do
    {
        history.Add(GetDifferencesBetweenValues(history.Last()));
    } while (history.Last().Any(value => value != 0));

    history.Last().Insert(0, 0);
    
    for (var i = history.Count - 2; i >= 0; i--)
    {
        var previousLineValue = history[i + 1].First();
        var currentLineValue = history[i].First();

        var nextValue = currentLineValue - previousLineValue;
        
        history[i].Insert(0, nextValue);
    }
    
    extrapolatedValues.Add(history.First().First());
}

Console.WriteLine($"Day 9 - Part 2: {extrapolatedValues.Sum()}");

List<int> GetDifferencesBetweenValues(List<int> values)
{
    var differences = new List<int>(values.Count - 1);

    for (var i = 1; i < values.Count; i++)
    {
        differences.Add(values[i] - values[i - 1]);
    }

    return differences;
}