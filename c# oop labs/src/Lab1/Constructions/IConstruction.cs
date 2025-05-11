using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Constructions;

public interface IConstruction
{
    public ResultType PassingConstruction(Train train);
}