using SpaceRace.Features.GateSequence.Models;

namespace SpaceRace.Features.GateSequence.Services;

/// <inheritdoc />
public sealed class SequenceDecodeService : ISequenceDecodeService
{
    public IReadOnlyList<int> CountSmallerToRight(IReadOnlyList<int> sequence)
    {
        ArgumentNullException.ThrowIfNull(sequence);

        var counts = new int[sequence.Count];
        for (int i = 0; i < sequence.Count; i++)
        {
            int smaller = 0;
            for (int j = i + 1; j < sequence.Count; j++)
            {
                if (sequence[j] < sequence[i])
                    smaller++;
            }
            counts[i] = smaller;
        }

        return counts;
    }

    public int RoundedUpMedian(IReadOnlyList<int> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        if (values.Count == 0)
            throw new ArgumentException("Cannot take the median of an empty sequence.", nameof(values));

        int[] sorted = values.OrderBy(v => v).ToArray();
        int mid = sorted.Length / 2;

        // Odd count: the single middle element is already an integer.
        if (sorted.Length % 2 == 1)
            return sorted[mid];

        // Even count: average the two middle elements and round up.
        double median = (sorted[mid - 1] + sorted[mid]) / 2.0;
        return (int)Math.Ceiling(median);
    }

    public SequenceDecodeReport Decode(IEnumerable<IReadOnlyList<int>> sequences)
    {
        ArgumentNullException.ThrowIfNull(sequences);

        var lines = new List<SequenceDecode>();
        long sum = 0;
        foreach (IReadOnlyList<int> sequence in sequences)
        {
            IReadOnlyList<int> counts = CountSmallerToRight(sequence);
            int median = RoundedUpMedian(counts);
            lines.Add(new SequenceDecode(sequence, counts, median));
            sum += median;
        }

        if (lines.Count == 0)
            throw new ArgumentException("At least one sequence is required.", nameof(sequences));

        return new SequenceDecodeReport(lines, sum);
    }
}
