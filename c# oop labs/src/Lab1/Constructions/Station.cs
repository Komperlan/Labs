using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Constructions;

public class Station : IConstruction
{
    public Station(MeterPerSecond maxSpeed)
    {
        MaxSpeed = maxSpeed;
    }

    public Second TimeToPassangersMove { get; private set; }

    private MeterPerSecond MaxSpeed { get; }

    ResultType IConstruction.PassingConstruction(Train train)
    {
        if (CheckSpeed(train.Speed))
        {
            return new ResultType(new Second(0), false);
        }

        return new ResultType(TimeToPassangersMove, true);
    }

    public bool CheckSpeed(MeterPerSecond speed)
    {
        return speed > MaxSpeed;
    }
}