using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;
using Itmo.ObjectOrientedProgramming.Lab4.Render;

namespace Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;

public class FileSystemShowerParameterHandler : ParameterHandlerBase
{
    public override ICommands? Handle(IEnumerator<string> request, IFileManager manager)
    {
        if (request.Current is not "tree")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        if (request.Current is not "list")
            return Next?.Handle(request, manager);

        if (request.MoveNext() is false)
            return null;

        IRenderAble render = new ConsoleRenderer();

        if (request.Current is "console")
            render = new ConsoleRenderer();

        if (request.MoveNext() is false)
            return null;

        if (request.Current is not "-d")
            return new FileSystemShower(1, render, manager);

        if (request.MoveNext() is false)
            return null;

        int depth = 0;

        int.TryParse(request.Current, out depth);

        return new FileSystemShower(depth, render, manager);
    }
}
