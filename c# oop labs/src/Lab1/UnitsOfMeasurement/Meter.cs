namespace Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

public class Meter
{
    public float Value { get; private set; }

    public Meter(float value)
    {
        if (value < 0)
            throw new ArgumentException("The length cannot be negative");

        Value = value;
    }

    public static Meter operator +(Meter lhs, Meter rhs) => new Meter(lhs.Value + rhs.Value);

    public static Meter operator -(Meter lhs, Meter rhs) => new Meter(lhs.Value - rhs.Value);

    public static bool operator <(Meter lhs, float rhs) => lhs.Value < rhs;

    public static bool operator >(Meter lhs, float rhs) => lhs.Value > rhs;
}
