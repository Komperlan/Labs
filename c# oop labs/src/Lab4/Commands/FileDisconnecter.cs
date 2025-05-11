using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDisconnecter : ICommands
{
    private readonly IFileManager _manager;

    public FileDisconnecter(IFileManager manager)
    {
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.DisconnectSystem(_manager);
    }
}