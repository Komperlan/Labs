using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.ConsoleHandlers;
using Itmo.ObjectOrientedProgramming.Lab4.FileManagers.LocalFileManagers;
using Itmo.ObjectOrientedProgramming.Lab4.OutputRunners;

using Xunit;

namespace Lab4.Tests;

public class ParserTest
{
    private readonly IParameterHandler handler = new ConnecterParameterHandler().AddNext(
            new CopyParameterHandler().AddNext(
                new DeleteParameterHandler().AddNext(
                    new DisconnecterParameterHandler().AddNext(
                        new FileMoverParameterHandler().AddNext(
                            new FileShowerParameterHandler().AddNext(
                                new FileSystemMoverParameterHandler().AddNext(
                                    new FileSystemShowerParameterHandler().AddNext(
                                        new RenameParameterHandler()))))))));

    [Fact]
    public void Parser_ShouldReturnTrue_WhenConnectedIsCorrect()
    {
        IEnumerable<string> args = ["connect", "twitch", "local"];
        var manager = new LocalFileManager(" ", new LocalFileComands());
        var runner = new OutputRunner(handler, manager);
        runner.Run(args);

        Assert.Equal("twitch", manager.Path);
    }

    [Fact]
    public void Parser_ShouldReturnTrue_WhenCopyCommandsIsExecuted()
    {
        IEnumerable<string> args = ["file", "copy", "a", "z"];
        var manager = new LocalFileManager(" ", new LocalFileComands());
        var handle = new CopyParameterHandler();

        IEnumerator<string> requests = args.GetEnumerator();

        requests.MoveNext();

        ICommands? command = handle.Handle(requests, manager);
        Assert.NotNull(command);
        FileCopier fileCopier = Assert.IsType<FileCopier>(command);
        fileCopier.Equals(new FileCopier("a", "z", manager));
    }
}
