using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileMover : ICommands
{
    private readonly string _sourcePath;

    private readonly string _destinationPath;

    private readonly IFileManager _manager;

    public FileMover(string sourcePath, string destinationPath, IFileManager manager)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.MoveFile(_sourcePath, _destinationPath, _manager);
    }
}