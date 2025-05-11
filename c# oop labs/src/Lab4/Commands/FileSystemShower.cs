using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;
using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileSystemShower : ICommands
{
    public int Depth { get; }

    private readonly IRenderAble _renderer;

    private readonly IFileManager _manager;

    public FileSystemShower(int depth, IRenderAble renderer, IFileManager manager)
    {
        Depth = depth;
        _renderer = renderer;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.ShowFileSystem(Depth, _manager.Path, _renderer, _manager);
    }
}
