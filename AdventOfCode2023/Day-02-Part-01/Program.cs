var sumOfIdsOfPossibleGames = File.ReadAllLines("./input.txt")
    .Select(ParseGameFromInput)
    .Where(GameWasPossible)
    .Sum(game => game.Id);

Console.WriteLine($"Day 2 - Part 1: {sumOfIdsOfPossibleGames}");
    
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

bool GameWasPossible(Game game)
{
    const int maxNumberOfRedCubes = 12;
    const int maxNumberOfGreenCubes = 13;
    const int maxNumberOfBlueCubes = 14;

    var colourMax = new Dictionary<CubeColour, int>
    {
        { CubeColour.Red, maxNumberOfRedCubes },
        { CubeColour.Blue, maxNumberOfBlueCubes },
        { CubeColour.Green, maxNumberOfGreenCubes }
    };

    return game.SubSets.All(cubeSubSets => cubeSubSets.Cubes.All(cube => cube.Count <= colourMax[cube.Colour]));
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
