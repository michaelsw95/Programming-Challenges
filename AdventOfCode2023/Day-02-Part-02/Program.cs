var sumOfGameCubePowers = File.ReadAllLines("./input.txt")
    .Select(ParseGameFromInput)
    .Sum(GetPowerOfCubeForGame);

Console.WriteLine($"Day 2 - Part 2: {sumOfGameCubePowers}");
    
Game ParseGameFromInput(string inputLine)
{
    var gameAndSubSetsInput = inputLine.Split(':');
    
    var gameId = int.Parse(gameAndSubSetsInput[0].Substring("Game ".Length - 1));

    var subSetsInput = gameAndSubSetsInput[1].Trim().Split(';');

    var gameSubSets = new GameSubSet[subSetsInput.Length];
    for (var i = 0; i < subSetsInput.Length; i++)
    {
        var cubes = subSetsInput[i].Split(',').Select(currentCube =>
        {
            var cubeParts = currentCube.Trim().Split(' ');

            return new CubeAppearance(GetCubeColourFromInput(cubeParts[1]), int.Parse(cubeParts[0]));
        }).ToArray();
        
        gameSubSets[i] = new GameSubSet(cubes);
    }

    return new Game(gameId, gameSubSets);
}

int GetPowerOfCubeForGame(Game game)
{
    var cubeMinimums = new Dictionary<CubeColour, int>
    {
        { CubeColour.Red, 0 },
        { CubeColour.Green, 0 },
        { CubeColour.Blue, 0 }
    };

    foreach (var cube in game.SubSets.SelectMany(set => set.Cubes))
    {
        if (cube.Count > cubeMinimums[cube.Colour])
        {
            cubeMinimums[cube.Colour] = cube.Count;
        }
    }

    return cubeMinimums[CubeColour.Red] * cubeMinimums[CubeColour.Green] * cubeMinimums[CubeColour.Blue];
}


CubeColour GetCubeColourFromInput(string input) => input switch
{
    "red" => CubeColour.Red,
    "green" => CubeColour.Green,
    "blue" => CubeColour.Blue,
    _ => throw new NotSupportedException()
};

enum CubeColour
{
    Red,
    Green,
    Blue
}

record Game(int Id, GameSubSet[] SubSets);

record GameSubSet(CubeAppearance[] Cubes);

record CubeAppearance(CubeColour Colour, int Count);
