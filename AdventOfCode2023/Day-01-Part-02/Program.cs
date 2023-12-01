var numbers = new List<(string Name, char Value)>
{
    ( "one", '1' ),
    ( "two", '2' ),
    ( "three", '3' ),
    ( "four", '4' ),
    ( "five", '5' ),
    ( "six", '6' ),
    ( "seven", '7' ),
    ( "eight", '8' ),
    ( "nine", '9' )
};

var sumOfCalibrationValues = File.ReadAllLines("./input.txt")
    .Select(calibration => {
        var startNumber = default(char);
        for (var i = 0; i < calibration.Length; i++)
        {
            if (char.IsDigit(calibration[i]))
            {
                startNumber = calibration[i];
                break;
            }
            
            if (StartsWithNumberName(calibration[i..], out var resultNumber))
            {
                startNumber = resultNumber;
                break;
            }
        }

        var endNumber = default(char);
        for (var i = calibration.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(calibration[i]))
            {
                endNumber = calibration[i];
                break;
            }
            else if (EndsWithNumberName(calibration.Substring(0, i + 1), out var resultNumber))
            {
                endNumber = resultNumber;
                break;
            }
        }

        return $"{startNumber}{endNumber}";
    })
    .Select(calibration =>
    {
        var digits = calibration.Where(char.IsDigit).ToArray();

        return $"{digits.First()}{digits.Last()}";
    })
    .Sum(int.Parse);

Console.WriteLine($"Day 1 - Part 2: {sumOfCalibrationValues}");
return;

bool StartsWithNumberName(string input, out char resultNumber)
{
    foreach (var number in numbers.Where(number => input.StartsWith(number.Name)))
    {
        resultNumber = number.Value;
        return true;
    }

    resultNumber = default(char);
    return false;
}

bool EndsWithNumberName(string input, out char resultNumber)
{
    foreach (var number in numbers.Where(number => input.EndsWith(number.Name)))
    {
        resultNumber = number.Value;
        return true;
    }

    resultNumber = default(char);
    return false;
}
