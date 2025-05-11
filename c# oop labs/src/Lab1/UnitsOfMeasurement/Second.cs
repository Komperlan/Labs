namespace Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

public struct Second
{
    public float Value { get; private set; }

    public Second(float value)
    {
        if (value < 0)
            throw new ArgumentException("Time cannot be negative");
        Value = value;
    }

    public static Second operator +(Second lhs, Second rhs) => new Second(lhs.Value + rhs.Value);
}
