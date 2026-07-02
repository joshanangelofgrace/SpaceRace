namespace SpaceRace.Features.EngineDialTicks.Models;

/// <summary>
/// One set of engine dials: the current readings and the expected readings the
/// central computer requires for safe atmospheric entry.
/// </summary>
public sealed class DialSet
{
    public List<Dial> Current { get; }
    public List<Dial> Expected { get; }

    public DialSet(List<Dial> current, List<Dial> expected)
    {
        ArgumentNullException.ThrowIfNull(current);
        ArgumentNullException.ThrowIfNull(expected);

        if (current.Count != expected.Count)
            throw new ArgumentException(
                $"Dial count mismatch: {current.Count} current vs {expected.Count} expected.");

        if (current.Count == 0)
            throw new ArgumentException("A dial set must contain at least one dial.");

        Current = current;
        Expected = expected;
    }

    /// <summary>
    /// The sum of the least ticks needed to bring every dial in this set from its
    /// current reading to its expected reading.
    /// </summary>
    public int TotalLeastTicks()
    {
        int total = 0;
        for (int i = 0; i < Current.Count; i++)
            total += Current[i].LeastTicksTo(Expected[i]);
        return total;
    }
}
