using System.Numerics;
using SpaceRace.Features.EngineDialTicks.Models;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <inheritdoc />
public sealed class EngineAdjustmentService : IEngineAdjustmentService
{
    public int CalculateSetTicks(DialSet set)
    {
        ArgumentNullException.ThrowIfNull(set);
        return set.TotalLeastTicks();
    }

    public BigInteger CalculateFinalResult(IEnumerable<DialSet> sets)
    {
        ArgumentNullException.ThrowIfNull(sets);

        BigInteger result = BigInteger.One;
        bool any = false;
        foreach (DialSet set in sets)
        {
            var total = CalculateSetTicks(set);
            result *= Math.Abs(total);
            any = true;
        }

        if (!any)
            throw new ArgumentException("At least one dial set is required.", nameof(sets));

        return result;
    }
}
