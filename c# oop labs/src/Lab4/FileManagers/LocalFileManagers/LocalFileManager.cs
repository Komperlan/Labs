namespace Itmo.ObjectOrientedProgramming.Lab4.FileManagers.LocalFileManagers;

public class LocalFileManager : IFileManager
{
    public string Path { get; set; }

    public IStrategy Strategy { get; }

    public bool IsConnected { get; set; }

    public LocalFileManager(string path, IStrategy strategy)
    {
        Path = path;
        Strategy = strategy;
    }
}
