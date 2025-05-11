namespace Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

public class MeterPerSecond
{
    public float Value { get; private set; } = 0;

    public MeterPerSecond(float speed)
    {
        if (speed < 0)
            throw new ArgumentException("speed cannot be negative");
        Value = speed;
    }

    public static MeterPerSecond operator +(MeterPerSecond lhs, MeterPerSecond rhs) => new MeterPerSecond(lhs.Value + rhs.Value);

    public static MeterPerSecond operator -(MeterPerSecond lhs, MeterPerSecond rhs) => new MeterPerSecond(lhs.Value - rhs.Value);

    public static bool operator <(MeterPerSecond lhs, MeterPerSecond rhs) => lhs.Value < rhs.Value;

    public static bool operator >(MeterPerSecond lhs, MeterPerSecond rhs) => lhs.Value > rhs.Value;

    public static bool operator <=(MeterPerSecond lhs, MeterPerSecond rhs) => lhs.Value <= rhs.Value;

    public static bool operator >=(MeterPerSecond lhs, MeterPerSecond rhs) => lhs.Value >= rhs.Value;
}
