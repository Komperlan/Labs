using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileSystemConnecter : ICommands
{
    public string Path { get; }

    private readonly IFileManager _manager;

    public FileSystemConnecter(string path, IFileManager manager)
    {
        Path = path;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.ConnectSystem(Path, _manager);
    }
}
