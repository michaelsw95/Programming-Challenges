var strategyGuideToOurChoiceMapping = new Dictionary<StrategyGuide, Choice>
{
    { new StrategyGuide(Choice.Rock, Outcome.Win), Choice.Paper },
    { new StrategyGuide(Choice.Paper, Outcome.Win), Choice.Scissors },
    { new StrategyGuide(Choice.Scissors, Outcome.Win), Choice.Rock },
    { new StrategyGuide(Choice.Paper, Outcome.Lose), Choice.Rock },
    { new StrategyGuide(Choice.Scissors, Outcome.Lose), Choice.Paper },
    { new StrategyGuide(Choice.Rock, Outcome.Lose), Choice.Scissors },
};

var predictedScore = File.ReadAllLines("./input.txt")
    .Select((string lineFromGuide) =>
    {
        var lineParts = lineFromGuide.Split(' ');

        return new StrategyGuide(ParseChoiceFromInput(lineParts[0]), ParseDesiredOutcomeFromInput(lineParts[1]));
    })
    .Select(GetScoreFromStrategyGuide)
    .Sum();

Console.WriteLine($"Day 2 - Part 2: {predictedScore}");

Choice ParseChoiceFromInput(string input) => input switch
{
    "A" => Choice.Rock,
    "B" => Choice.Paper,
    "C" => Choice.Scissors,
    _ => throw new NotSupportedException()
};

Outcome ParseDesiredOutcomeFromInput(string input) => input switch
{
    "X" => Outcome.Lose,
    "Y" => Outcome.Draw,
    "Z" => Outcome.Win,
    _ => throw new NotSupportedException()
};

int GetScoreFromStrategyGuide(StrategyGuide strategyGuide) =>
    (int)strategyGuide.DesiredOutcome + (int)GetOurChoiceFromStrategyGuide(strategyGuide);

Choice GetOurChoiceFromStrategyGuide(StrategyGuide strategyGuide) =>
    strategyGuide.DesiredOutcome == Outcome.Draw ? strategyGuide.OpponentChoice : strategyGuideToOurChoiceMapping[strategyGuide];

enum Choice
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Outcome
{
    Lose = 0,
    Draw = 3,
    Win = 6
}

record StrategyGuide(Choice OpponentChoice, Outcome DesiredOutcome);
