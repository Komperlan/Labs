namespace Itmo.ObjectOrientedProgramming.Lab4.Render;

public class ConsoleRenderer : IRenderAble
{
    public void Render(FileInfo file)
    {
        Console.WriteLine(file);
    }

    public void Render(string info)
    {
        Console.WriteLine(info);
    }
}
