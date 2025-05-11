using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;
using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileShower : ICommands
{
    private readonly string _sourcePath;

    private readonly IFileManager _manager;

    public IRenderAble Renderer { get; }

    public FileShower(string sourcePath, IRenderAble renderer, IFileManager manager)
    {
        _sourcePath = sourcePath;
        Renderer = renderer;
        _manager = manager;
    }

    public void Execute()
    {
        _manager.Strategy.ShowFile(_sourcePath, Renderer, _manager);
    }
}