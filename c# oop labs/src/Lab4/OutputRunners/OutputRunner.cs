using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.OutputRunners;

public class OutputRunner : IOutputRunner
{
    private readonly IParameterHandler _handler;

    private readonly IFileManager _manager;

    public OutputRunner(IParameterHandler handler, IFileManager manager)
    {
        _handler = handler;
        _manager = manager;
    }

    public void Run(IEnumerable<string> args)
    {
        using IEnumerator<string> request = args.GetEnumerator();

        while (request.MoveNext())
        {
            ICommands? nextModifier = _handler.Handle(request, _manager);
            if (nextModifier != null)
                nextModifier.Execute();
        }
    }
}