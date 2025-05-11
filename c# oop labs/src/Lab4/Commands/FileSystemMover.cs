using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileSystemMover : ICommands
{
    public string Path { get; }

    private readonly IFileManager _manager;

    public FileSystemMover(string path, IFileManager manager)
    {
        Path = path;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.MoveSystem(Path, _manager);
    }
}
