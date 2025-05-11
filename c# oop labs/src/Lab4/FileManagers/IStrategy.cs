using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

public interface IStrategy
{
    public void Copy(string sourcePath, string destinationPath, IFileManager manager);

    public void Delete(string path, IFileManager manager);

    public void MoveFile(string sourcePath, string destinationPath, IFileManager manager);

    public void MoveSystem(string path, IFileManager manager);

    public void ConnectSystem(string path, IFileManager manager);

    public void DisconnectSystem(IFileManager manager);

    public void ShowFileSystem(int depth, string sourcePath, IRenderAble renderer, IFileManager manager);

    public void ShowFile(string sourcePath, IRenderAble renderer, IFileManager manager);

    public void RenameFile(string sourcePath, IFileManager manager);
}
