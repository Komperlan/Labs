using Crayon;
using static Crayon.Output;

namespace Itmo.ObjectOrientedProgramming.Lab3.Adressee.Displays;

public class DisplayDriver
{
    public IOutput Color { get; set; } = White();

    public void Clear()
    {
        Console.Clear();
    }
}