public class Board
{
    public static int boardSize = 5;
    private readonly List<(int guess, bool found)[]> _board;
    private int _lastSeenGuess;

    public Board()
    {
        _board = new List<(int, bool)[]>(5);
    }

    public void AddRow(IEnumerable<int> row)
    {
        _board.Add(row.Select(guess => (guess, false)).ToArray());
    }

    public void CheckNumber(int guess)
    {
        _lastSeenGuess = guess;

        for (int i = 0; i < _board.Count; i++)
        {
            for (int j = 0; j < _board[i].Length; j++)
            {
                if (_board[i][j].guess == guess) 
                {
                    _board[i][j].found = true;
                }
            }
        }
    }

    public bool IsWinner()
    {
        for (var i = 0; i < boardSize; i++)
        {
            var rowWins = _board[i].All(boardItem => boardItem.found);

            if (rowWins)
            {
                return true;
            }
        }

        for (var i = 0; i < boardSize; i++)
        {
            var column = new (int, bool found)[5] {
                _board[0][i], _board[1][i], _board[2][i], _board[3][i], _board[4][i]
            };

            if (column.All(boardItem => boardItem.found))
            {
                return true;
            }
        }

        return false;
    }

    public int GetScore()
    {
        var unmarkedNumbers = new List<int>();

        foreach (var row in _board)
        {
            foreach (var item in row)
            {
                if (!item.found)
                {
                    unmarkedNumbers.Add(item.guess);
                }
            }
        }

        return unmarkedNumbers.Sum() * _lastSeenGuess;
    }
}