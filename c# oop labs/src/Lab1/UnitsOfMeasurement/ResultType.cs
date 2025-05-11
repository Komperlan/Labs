namespace Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

public class ResultType
{
    public Second Time { get; }

    public bool Succes { get; }

    public ResultType(Second time, bool isSucces)
    {
        Succes = isSucces;
        Time = time;
    }
}
