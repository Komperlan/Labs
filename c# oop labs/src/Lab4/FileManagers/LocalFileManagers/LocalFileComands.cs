using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.FileManagers.LocalFileManagers;

public class LocalFileComands : IStrategy
{
    public void Copy(string sourcePath, string destinationPath, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(sourcePath))
        {
            file = new FileInfo(sourcePath);
        }
        else
        {
            file = new FileInfo(manager.Path + sourcePath);
        }

        if (checker.CheckIsAbsolutePath(destinationPath))
        {
            file.CopyTo(destinationPath);
        }
        else
        {
            file.CopyTo(manager.Path + destinationPath);
        }
    }

    public void Delete(string path, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(path))
        {
            file = new FileInfo(path);
        }
        else
        {
            file = new FileInfo(manager.Path + path);
        }

        file.Delete();
    }

    public void MoveFile(string sourcePath, string destinationPath, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(sourcePath))
        {
            file = new FileInfo(sourcePath);
        }
        else
        {
            file = new FileInfo(manager.Path + sourcePath);
        }

        if (checker.CheckIsAbsolutePath(destinationPath))
        {
            file.MoveTo(destinationPath);
        }
        else
        {
            file.MoveTo(manager.Path + destinationPath);
        }
    }

    public void MoveSystem(string path, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(path))
        {
            file = new FileInfo(path);
        }
        else
        {
            file = new FileInfo(manager.Path + path);
        }
    }

    public void ConnectSystem(string path, IFileManager manager)
    {
        manager.Path = path;
    }

    public void DisconnectSystem(IFileManager manager)
    {
        manager.Path = " ";
        manager.IsConnected = false;
    }

    public void ShowFileSystem(int depth, string sourcePath, IRenderAble renderer, IFileManager manager)
    {
        var shower = new TreeShower(depth, renderer);
        shower.PrintTree(manager.Path);
    }

    public void ShowFile(string sourcePath, IRenderAble renderer, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(sourcePath))
        {
            file = new FileInfo(sourcePath);
        }
        else
        {
            file = new FileInfo(manager.Path + sourcePath);
        }

        renderer.Render(file);
    }

    public void RenameFile(string sourcePath, IFileManager manager)
    {
        var checker = new PathChecker();
        FileInfo file;
        if (checker.CheckIsAbsolutePath(sourcePath))
        {
            file = new FileInfo(sourcePath);
        }
        else
        {
            file = new FileInfo(manager.Path + sourcePath);
        }
    }
}
