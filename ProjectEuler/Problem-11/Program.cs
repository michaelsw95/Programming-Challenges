var gridTextLines = File.ReadAllLines("./input.txt");

var grid = gridTextLines.Select(line => line.Split(" ").Select(gridValue => int.Parse(gridValue)).ToArray()).ToArray();

var gridHeight = grid.Length;
var gridWidth = grid.First().Length;

var largestFoundProduct = 0;

for (var i = 0; i < gridHeight; i++)
{
    for (var j = 0; j < gridWidth; j++)
    {
        var productDiagonalBack = GetProductOfFourNeighbours((i, j), incrementI: 1, incrementJ: -1);
        var verticleProduct = GetProductOfFourNeighbours((i, j), incrementI: 1, incrementJ: 0);
        var productDiagonalForward = GetProductOfFourNeighbours((i, j), incrementI: 1, incrementJ: 1);
        var productHorizontal = GetProductOfFourNeighbours((i, j), incrementI: 0, incrementJ: 1);

        largestFoundProduct = ReplaceIfGreater(productDiagonalBack, verticleProduct, productDiagonalForward, productHorizontal);
    }
}

Console.WriteLine($"Project Euler - Problem 11: {largestFoundProduct}");

int ReplaceIfGreater(params int[] products) => Math.Max(largestFoundProduct, products.Max());

int GetProductOfFourNeighbours(
    (int i, int j) position,
    int incrementI,
    int incrementJ)
    {
        if (PositionIsOutOfBoundsOfGrid((position.i + (incrementI * 3), (position.j + (incrementJ * 3)))))
        {
            return 0;
        }

        return grid[position.i][position.j] * 
            grid[position.i + incrementI][ position.j + incrementJ] *
            grid[position.i + (incrementI * 2)][position.j + (incrementJ * 2)] *
            grid[position.i + (incrementI * 3)][position.j + (incrementJ * 3)];
    }

bool PositionIsOutOfBoundsOfGrid((int i, int j) position) =>
    (position.i < 0 || position.i >= gridHeight) || (position.j < 0 || position.j >= gridWidth);
