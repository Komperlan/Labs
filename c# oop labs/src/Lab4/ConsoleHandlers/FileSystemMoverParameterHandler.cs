using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;

public class FileSystemMoverParameterHandler : ParameterHandlerBase
{
    public override ICommands? Handle(IEnumerator<string> request, IFileManager manager)
    {
        if (request.Current is not "tree")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        if (request.Current is not "goto")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        string? sourcePath = request.Current;

        if (sourcePath is null)
            return Next?.Handle(request, manager);

        return new FileSystemMover(sourcePath, manager);
    }
}
