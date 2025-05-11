namespace Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

public class Kilogram
{
    public float Value { get; private set; }

    public Kilogram(float value)
    {
        if (value < 0)
            throw new ArgumentException("The weight cannot be negative");

        Value = value;
    }

    public static Kilogram operator +(Kilogram lhs, Kilogram rhs) => new Kilogram(lhs.Value + rhs.Value);

    public static Kilogram operator -(Kilogram lhs, Kilogram rhs) => new Kilogram(lhs.Value - rhs.Value);

    public static bool operator <(Kilogram lhs, float rhs) => lhs.Value < rhs;

    public static bool operator >(Kilogram lhs, float rhs) => lhs.Value > rhs;
}
