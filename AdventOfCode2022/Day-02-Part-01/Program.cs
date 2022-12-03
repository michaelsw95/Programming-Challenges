var rockPaperScissorResultMapping = new Dictionary<PlayerChoice, Outcome>
{
    { new PlayerChoice(Choice.Rock, Choice.Paper), Outcome.Won },
    { new PlayerChoice(Choice.Paper, Choice.Scissors), Outcome.Won },
    { new PlayerChoice(Choice.Scissors, Choice.Rock), Outcome.Won },
    { new PlayerChoice(Choice.Paper, Choice.Rock), Outcome.Lost },
    { new PlayerChoice(Choice.Scissors, Choice.Paper), Outcome.Lost },
    { new PlayerChoice(Choice.Rock, Choice.Scissors), Outcome.Lost },
};

var predictedScore = File.ReadAllLines("./input.txt")
    .Select((string lineFromGuide) =>
    {
        var lineParts = lineFromGuide.Split(' ');

        return new PlayerChoice(ParseChoiceFromInput(lineParts[0]), ParseChoiceFromInput(lineParts[1]));
    })
    .Select(GetScoreFromPlayerChoices)
    .Sum();

Console.WriteLine($"Day 2 - Part 1: {predictedScore}");

Choice ParseChoiceFromInput(string input) => input switch
{
    "A" => Choice.Rock,
    "B" => Choice.Paper,
    "C" => Choice.Scissors,
    "X" => Choice.Rock,
    "Y" => Choice.Paper,
    "Z" => Choice.Scissors,
    _ => throw new NotSupportedException()
};

int GetScoreFromPlayerChoices(PlayerChoice playerChoice) =>
    (int)GetOutcomeFromPlayerChoices(playerChoice) + (int)playerChoice.OurChoice;

Outcome GetOutcomeFromPlayerChoices(PlayerChoice playerChoice) =>
    playerChoice.OpponentChoice == playerChoice.OurChoice ? Outcome.Draw : rockPaperScissorResultMapping[playerChoice];

enum Choice
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

enum Outcome
{
    Lost = 0,
    Draw = 3,
    Won = 6
}

record PlayerChoice(Choice OpponentChoice, Choice OurChoice);