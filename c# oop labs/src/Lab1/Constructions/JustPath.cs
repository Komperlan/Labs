using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Constructions;

public class JustPath : IConstruction
{
    public Meter Range { get; internal set; }

    public JustPath(Meter range)
    {
        Range = range;
    }

    ResultType IConstruction.PassingConstruction(Train train)
    {
        float iteration = 0;

        if (train.NoHaveSpeedOrBoost())
            return new ResultType(new Second(0), false);

        while (Range > 0)
        {
            ChangeRange(train.TraveledRange());
            ++iteration;
        }

        iteration += train.RangeOverLastPath.Value / train.Speed.Value;

        return new ResultType(new Second(iteration), true);
    }

    internal void ChangeRange(Meter range)
    {
        Range -= range;
    }
}
