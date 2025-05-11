using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

namespace Itmo.ObjectOrientedProgramming.Lab1.Trains;

public class Train
{
    public MeterPerSecond Speed { get; private set; }

    public Kilogram Weight { get; }

    public float MaxForce { get; }

    public Second Accuracy { get; }

    public float Boost { get; private set; } = 0;

    public Meter RangeOverLastPath { get; private set; }

    public Train(float weight, float maxForce, float accuracy)
    {
        if (maxForce < 0)
            throw new ArgumentException("Force cannot be negative");

        Weight = new Kilogram(weight);
        MaxForce = maxForce;
        Accuracy = new Second(accuracy);
        Speed = new MeterPerSecond(0);
        RangeOverLastPath = new Meter(0);
    }

    public void ChangeSpeed()
    {
        float changedSpeed = Boost * Accuracy.Value;

        if (changedSpeed >= 0)
        {
            Speed += new MeterPerSecond(changedSpeed);
        }
        else
        {
            Speed -= new MeterPerSecond(-1 * changedSpeed);
        }
    }

    public bool ChangeBoost(float force)
    {
        if (force > MaxForce)
            return false;

        Boost = force / Weight.Value;
        return true;
    }

    public Meter TraveledRange()
    {
        return new Meter(Speed.Value * Accuracy.Value);
    }

    public bool NoHaveSpeedOrBoost()
    {
        return Speed.Value <= 0 && Boost <= 0;
    }
}