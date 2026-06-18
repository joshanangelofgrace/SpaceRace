using SpaceRace.Features.StonePiles.Models;
using System.Numerics;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>
/// Computes the maximum number of splits achievable for a pile and concatenates the
/// per-row results into the final answer.
/// </summary>
public interface ISplitCalculatorService
{
    /// <summary>Maximum number of splits for a single pile using the given factor set.</summary>
    BigInteger MaxSplits(BigInteger pileSize, IReadOnlyList<BigInteger> set);

    /// <summary>Evaluates every pile and concatenates the per-row results.</summary>
    PileSplitReport Evaluate(IEnumerable<StonePile> piles);
}
 