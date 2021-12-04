var gameInput = File.ReadAllLines("./input.txt");

var drawnNumbers = gameInput.First().Split(",").Select(draw => int.Parse(draw)).ToArray();

var boards = new List<Board>();

for (var i = 2; i < gameInput.Length; i += (Board.boardSize + 1))
{
    var board = new Board();

    for (var j = 0; j < Board.boardSize; j++)
    {
        var row = gameInput[i + j].Split(" ")
            .Where(draw => !string.IsNullOrWhiteSpace(draw))
            .Select(draw => int.Parse(draw));

        board.AddRow(row);
    }

    boards.Add(board);
}

var winnerScore = 0;

foreach (var number in drawnNumbers)
{
    foreach (var board in boards)
    {
        board.CheckNumber(number);

        if (board.IsWinner())
        {
            winnerScore = board.GetScore();
            break;
        }
    }

    if (winnerScore > 0)
    {
        break;
    }
}

Console.WriteLine($"Day 4 - Part 1: {winnerScore}");