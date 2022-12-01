var calories = File.ReadAllLines("./input.txt");

var elfCalories = new List<List<int>> { new List<int>() };
var elfPosition = 0;

foreach (var calorieValue in calories)
{
    if (string.Equals(calorieValue, string.Empty))
    {
        elfCalories.Add(new List<int>());

        elfPosition++;
    }
    else
    {
        elfCalories[elfPosition].Add(Convert.ToInt32(calorieValue));
    }
}

var topThreeHighestCalories = elfCalories
    .Select(currentElfCalories => currentElfCalories.Sum())
    .OrderByDescending(calorieSum => calorieSum)
    .Take(3)
    .Sum();

Console.WriteLine($"Day 1 - Part 2: {topThreeHighestCalories}");
