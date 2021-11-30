const int targetCountOfFactors = 501;

var triangleNumberPosition = 0;
var triangleNumber = 0;
var countOfFactors = 0;

do {
    triangleNumberPosition++;

    triangleNumber += triangleNumberPosition;

    countOfFactors = GetCountOfFactors(triangleNumber);
} while (countOfFactors <= targetCountOfFactors);

Console.WriteLine($"Project Euler - Problem 12: {triangleNumber}");

int GetCountOfFactors(int numberToCountFactors)
{
    var countOfFactors = 0;
    var sqrtOfTarget = Math.Ceiling(Math.Sqrt(numberToCountFactors));
    
    if (NumberIsExactSquare(sqrtOfTarget, numberToCountFactors))
    {
        countOfFactors++;
    }

    for (var i = Convert.ToInt32(sqrtOfTarget); i > 0; i--)
    {
        if (numberToCountFactors % i == 0)
        {
            countOfFactors += 2;
        }
    }

    return countOfFactors;

    bool NumberIsExactSquare(double sqrtOfTarget, int target) => sqrtOfTarget * sqrtOfTarget == target;
}