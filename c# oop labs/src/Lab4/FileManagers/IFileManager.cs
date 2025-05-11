namespace Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

public interface IFileManager
{
    public string Path { get; set; }

    public bool IsConnected { get; set; }

    public IStrategy Strategy { get; }
}
