using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDeleter : ICommands
{
    private readonly string _sourcePath;

    private readonly IFileManager _manager;

    public FileDeleter(string path, IFileManager manager)
    {
        _sourcePath = path;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.Delete(_sourcePath, _manager);
    }
}