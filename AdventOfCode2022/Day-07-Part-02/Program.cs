const int totalDiscSpace = 70000000;
const int totalUpdateSpace = 30000000;

const string cdCommand = "$ cd";
const string lsCommand = "$ ls";

var commands = File.ReadAllLines("./input.txt");

var fileSystem = GetParsedDirectoryFromInput(commands.First());

foreach (var command in commands.Skip(1))
{
    var commandParts = command.Split(' ');

    if (command.StartsWith(cdCommand))
    {
        fileSystem = commandParts.Last() == ".." ?
            fileSystem.Parent :
            fileSystem.Children.Single(childNodes => childNodes.Name == commandParts.Last());
    }
    else if (IsOutputFromListCommand(command))
    {
        var type = command.StartsWith("dir") ? DirectoryItemType.directory : DirectoryItemType.file;
        var size = type == DirectoryItemType.directory ? 0 : int.Parse(commandParts[0]);
        var name = commandParts[1];
        var children = type == DirectoryItemType.directory ? new List<Directory>() : null;

        fileSystem.Children.Add(new Directory(type, name, size, children, fileSystem));

        if (type == DirectoryItemType.file)
        {
            BackPopulateSizeToParentDirectories(fileSystem, size);
        }
    }
}

while (fileSystem.Parent != null)
{
    fileSystem = fileSystem.Parent;
}

var directories = FlattenFileSystem(fileSystem).OrderBy(directory => directory.Size);

var unusedDiscSpace = totalDiscSpace - directories.Max(directory => directory.Size);
var miniumumCleanupSpace = totalUpdateSpace - unusedDiscSpace;

var directoryToDelete = directories.First(directory => directory.Size >= miniumumCleanupSpace);

Console.WriteLine($"Day 7 - Part 2: {directoryToDelete.Size}");

Directory GetParsedDirectoryFromInput(string command, Directory parent = null) =>
    new Directory(DirectoryItemType.directory, command.Split(' ').Last(), 0, new List<Directory>(), parent);

bool IsOutputFromListCommand(string command) => !command.Equals(lsCommand) && !command.StartsWith(cdCommand);

void BackPopulateSizeToParentDirectories(Directory directory, int size)
{
    directory.Size += size;

    if (directory.Parent != null)
    {
        BackPopulateSizeToParentDirectories(directory.Parent, size);
    }
}

List<Directory> FlattenFileSystem(Directory directory)
{
    var directories = new List<Directory>();

    directories.Add(directory);

    foreach (var childFolder in directory.Children.Where(child => child.Type == DirectoryItemType.directory))
    {
        directories.AddRange(FlattenFileSystem(childFolder));
    }

    return directories;
}

class Directory
{
    public DirectoryItemType Type { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }
    public List<Directory> Children { get; set; }
    public Directory Parent { get; set; }

    public Directory(DirectoryItemType type, string name, int size, List<Directory> children, Directory parent)
    {
        Type = type;
        Name = name;
        Size = size;
        Children = children;
        Parent = parent;
    }
}

enum DirectoryItemType
{
    file,
    directory
}
