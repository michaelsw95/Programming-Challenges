var floorInstructions = File.ReadAllText("./input.txt");

var floorPosition = 0;
var firstBasementPosition = 0;

const int basementFloor = -1;

for (int i = 0; i < floorInstructions.Length; i++)
{
    floorPosition += floorInstructions[i] == '(' ? 1 : -1;
    
    if (floorPosition == basementFloor)
    {
        firstBasementPosition = i + 1;
        break;
    }
}

Console.WriteLine($"Day 1 - Part 2: {firstBasementPosition}");
