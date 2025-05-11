namespace Itmo.ObjectOrientedProgramming.Lab4.Render;

public interface IRenderAble
{
    void Render(FileInfo file);

    void Render(string info);
}