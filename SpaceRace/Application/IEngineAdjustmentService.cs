using System.Numerics;
using SpaceRace.Domain;

namespace SpaceRace.Application;

/// <summary>
/// Calculates how the spaceship engines must be adjusted to support the atmosphere.
/// </summary>
public interface IEngineAdjustmentService
{
    /// <summary>Sum of the least ticks for a single set of dials.</summary>
    int CalculateSetTicks(DialSet set);

    /// <summary>
    /// The product of every set's total least ticks — the final figure the central
    /// computer requires across all dial sets. Returned as a <see cref="BigInteger"/>
    /// because the product overflows 64-bit integers for realistic inputs.
    /// </summary>
    BigInteger CalculateFinalResult(IEnumerable<DialSet> sets);
}
