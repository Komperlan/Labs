using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Constructions;

public class Route
{
    public ICollection<IConstruction> Trail { get; private set; }

    private readonly MeterPerSecond _maxSpeed;

    public Route(ICollection<IConstruction> route, MeterPerSecond maxSpeed)
    {
        Trail = route;
        _maxSpeed = maxSpeed;
    }

    public bool CheckSpeed(MeterPerSecond speed)
    {
        return speed > _maxSpeed;
    }
}
