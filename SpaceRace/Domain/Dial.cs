namespace SpaceRace.Domain;

/// <summary>
/// A single engine throttle dial. The dial is continuous and numbered 0-9:
/// turning one tick right wraps 0 -> 9, one tick left wraps 9 -> 0.
/// </summary>
public readonly record struct Dial
{
    /// <summary>Number of distinct positions on the dial (0-9).</summary>
    public const int Size = 10;

    public int Value { get; }

    public Dial(int value)
    {
        if (value < 0 || value >= Size)
            throw new ArgumentOutOfRangeException(
                nameof(value), value, $"Dial value must be between 0 and {Size - 1}.");

        Value = value;
    }

    /// <summary>
    /// The least number of ticks (in either direction) required to turn this dial
    /// to the <paramref name="target"/> position.
    /// </summary>
    public int LeastTicksTo(Dial target)
    {
        int diff = Math.Abs(Value - target.Value);
        return Math.Min(diff, Size - diff);
    }

    public override string ToString() => Value.ToString();
}
