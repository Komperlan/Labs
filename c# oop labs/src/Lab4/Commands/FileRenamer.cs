using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileRenamer : ICommands
{
    private readonly string _sourcePath;

    private readonly IFileManager _manager;

    public FileRenamer(string path, IFileManager manager)
    {
        _sourcePath = path;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.RenameFile(_sourcePath, _manager);
    }
}