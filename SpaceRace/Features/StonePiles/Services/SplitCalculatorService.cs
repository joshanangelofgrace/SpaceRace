using System.Numerics;
using System.Text;
using SpaceRace.Features.StonePiles.Models;

namespace SpaceRace.Features.StonePiles.Services;

/// <inheritdoc />
public sealed class SplitCalculatorService : ISplitCalculatorService
{
    public BigInteger MaxSplits(BigInteger pileSize, IReadOnlyList<BigInteger> set)
    {
        ArgumentNullException.ThrowIfNull(set);
        if (pileSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pileSize), pileSize, "Pile size must be positive.");

        return Compute(pileSize, set, []);
    }   

    // f(n) = max over valid factors x of [ 1 + (n/x) * f(x) ], else 0.
    // A valid x is in the set, is a proper factor of n (x != n, n % x == 0), and is
    // positive — which guarantees x < n, so the recursion always terminates.
    private static BigInteger Compute(BigInteger n, IReadOnlyList<BigInteger> set, Dictionary<BigInteger, BigInteger> memo)
    {
        if (memo.TryGetValue(n, out BigInteger cached))
            return cached;

        BigInteger best = 0;
        foreach (BigInteger x in set)
        {
            if (x <= 0 || x == n || n % x != 0)
                continue;

            BigInteger splits = 1 + (n / x) * Compute(x, set, memo);
            if (splits > best)
                best = splits;
        }

        memo[n] = best;
        return best;
    }

    public PileSplitReport Evaluate(IEnumerable<StonePile> piles)
    {
        ArgumentNullException.ThrowIfNull(piles);

        var rows = new List<PileSplit>();
        var concatenated = new StringBuilder();
        foreach (StonePile pile in piles)
        {
            BigInteger max = MaxSplits(pile.Size, pile.Set);
            rows.Add(new PileSplit(pile, max));
            concatenated.Append(max);
        }

        if (rows.Count == 0)
            throw new ArgumentException("At least one pile is required.", nameof(piles));

        return new PileSplitReport(rows, concatenated.ToString());
    }
}
