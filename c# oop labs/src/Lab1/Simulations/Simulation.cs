using Itmo.ObjectOrientedProgramming.Lab1.Constructions;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Simulations;

public class Simulation
{
    private readonly Route _route;

    private readonly Train _train;

    public ResultType Result { get; private set; } = new ResultType(new Second(0), false);

    public Simulation(Route route, Train train)
    {
        _route = route;
        _train = train;
    }

    public void Simulate()
    {
        var pathTime = new Second(0);

        foreach (IConstruction element in _route.Trail)
        {
            ResultType passingResult = element.PassingConstruction(_train);

            if (!passingResult.Succes)
            {
                return;
            }

            pathTime += passingResult.Time;
        }

        if (_route.CheckSpeed(_train.Speed))
            return;

        Result = new ResultType(pathTime, true);
    }
}
