using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Constructions;

public class PowerPath : JustPath, IConstruction
{
    public PowerPath(Meter range, float power) : base(range)
    {
        Power = power;
    }

    public float Power { get; private set; }

    ResultType IConstruction.PassingConstruction(Train train)
    {
        if (!train.ChangeBoost(Power))
            return new ResultType(new Second(0), false);

        float iteration = 0;

        while (Range > 0)
        {
            train.ChangeSpeed();
            if (train.NoHaveSpeedOrBoost())
                return new ResultType(new Second(0), false);

            ChangeRange(train.TraveledRange());
            iteration++;
        }

        iteration += train.RangeOverLastPath.Value / train.Speed.Value;

        return new ResultType(new Second(iteration), true);
    }
}