using SpaceRace.Features.GateSequence.Models;

namespace SpaceRace.Features.GateSequence.Services;

/// <summary>
/// Decodes the gate puzzle: for each sequence, count how many numbers to the right of
/// each element are strictly smaller, then sum the rounded-up medians of those counts.
/// </summary>
public interface ISequenceDecodeService
{
    /// <summary>For each element, the count of later elements that are strictly smaller.</summary>
    IReadOnlyList<int> CountSmallerToRight(IReadOnlyList<int> sequence);

    /// <summary>The median of the values, rounded up (ceiling) when it falls on a half.</summary>
    int RoundedUpMedian(IReadOnlyList<int> values);

    /// <summary>Decodes every sequence and totals the rounded-up medians.</summary>
    SequenceDecodeReport Decode(IEnumerable<IReadOnlyList<int>> sequences);
}
