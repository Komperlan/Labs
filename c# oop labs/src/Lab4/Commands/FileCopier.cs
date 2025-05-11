using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileCopier : ICommands, IEquatable<FileCopier>
{
    public string SourcePath { get; }

    private readonly IFileManager _fileManager;

    public string DestinationPath { get; }

    public override bool Equals(object? obj) => Equals(obj as FileCopier);

    public bool Equals(FileCopier? other)
    {
        if (other == null)
            return false;

        if (GetHashCode() == other.GetHashCode())
        {
            return true;
        }

        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 166136261;
            hash = 16777619 * hash * (SourcePath?.GetHashCode(default) ?? 0);
            return hash;
        }
    }

    public FileCopier(string sourcePath, string destinationPath, IFileManager manager)
    {
        SourcePath = sourcePath;
        DestinationPath = destinationPath;
        _fileManager = manager;
    }

    public void Execute()
    {
        _fileManager.Strategy.Copy(SourcePath, DestinationPath, _fileManager);
    }
}