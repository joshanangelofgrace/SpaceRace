namespace SpaceRace.Features.GateSequence.Models;

/// <summary>One decoded sequence: the source numbers, the right-smaller counts, and
/// the rounded-up median of those counts.</summary>
public sealed record SequenceDecode(
    IReadOnlyList<int> Source,
    IReadOnlyList<int> Counts,
    int RoundedMedian);

/// <summary>The decoded sequences plus the sum of their rounded-up medians.</summary>
public sealed record SequenceDecodeReport(
    IReadOnlyList<SequenceDecode> Lines,
    long Sum);
