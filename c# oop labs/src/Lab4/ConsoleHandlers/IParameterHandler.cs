using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers;

namespace Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;

public interface IParameterHandler
{
    IParameterHandler AddNext(IParameterHandler handler);

    ICommands? Handle(IEnumerator<string> request, IFileManager manager);
}
