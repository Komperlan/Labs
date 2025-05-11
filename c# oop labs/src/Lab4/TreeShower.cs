using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4;

public class TreeShower
{
    public TreeShower(int maxDepth, IRenderAble renderer)
    {
        _renderer = renderer;
        MaxDepth = maxDepth;
    }

    public int MaxDepth { get; }

    private readonly IRenderAble _renderer;

    public void PrintTree(string startDir, string prefix = "", int depth = 0)
    {
        if (depth >= MaxDepth)
        {
            return;
        }

        var di = new DirectoryInfo(startDir);
        var fsItems = di.GetFileSystemInfos()
            .Where(f => true)
            .OrderBy(f => f.Name)
            .ToList();

        foreach (FileSystemInfo? fsItem in fsItems.Take(fsItems.Count - 1))
        {
            _renderer.Render(prefix + "├── ");
            _renderer.Render(fsItem.Name);
            _renderer.Render("\n");
            if ((fsItem.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                PrintTree(fsItem.FullName, prefix + "│   ", depth + 1);
            }
        }

        FileSystemInfo? lastFsItem = fsItems.LastOrDefault();
        if (lastFsItem != null)
        {
            _renderer.Render(prefix + "└── ");
            _renderer.Render(lastFsItem.Name);
            _renderer.Render("\n");
            if ((lastFsItem.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
            {
                PrintTree(lastFsItem.FullName, prefix + "    ", depth + 1);
            }
        }
    }
}
