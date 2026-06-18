using System.Numerics;

namespace SpaceRace.Features.StonePiles.Models;

/// <summary>One row of the puzzle: a pile of <see cref="Size"/> stones and the set of
/// distinct factors that may be used to split it.</summary>
public sealed record StonePile(BigInteger Size, IReadOnlyList<BigInteger> Set);

/// <summary>A pile together with the maximum number of splits achievable for it.</summary>
public sealed record PileSplit(StonePile Pile, BigInteger MaxSplits);

/// <summary>The per-row splits plus the concatenation of every row's result.</summary>
public sealed record PileSplitReport(IReadOnlyList<PileSplit> Rows, string Result);
