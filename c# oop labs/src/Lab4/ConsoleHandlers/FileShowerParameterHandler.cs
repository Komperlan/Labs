using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;
using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;

public class FileShowerParameterHandler : ParameterHandlerBase
{
    public override ICommands? Handle(IEnumerator<string> request, IFileManager manager)
    {
        if (request.Current is not "file")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        if (request.Current is not "show")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        string? sourcePath = request.Current;

        if (sourcePath is null)
            return Next?.Handle(request, manager);

        if (request.Current is not "Mode" && request.Current is not "-m")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        string? mode = request.Current;

        if (mode is null)
            return Next?.Handle(request, manager);

        if (mode == "console")
        {
            return new FileShower(sourcePath, new ConsoleRenderer(), manager);
        }

        return new FileShower(sourcePath, new ConsoleRenderer(), manager);
    }
}
