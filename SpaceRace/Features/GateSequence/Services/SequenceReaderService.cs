using SpaceRace.Infrastructure;

namespace SpaceRace.Features.GateSequence.Services;

/// <summary>
/// Reads integer sequences from <c>gatesequence.txt</c> (shipped with the solution and
/// copied to the output directory). Each non-blank line is one sequence, e.g.
/// <c>[5, 2, 6, 1]</c> or <c>5 2 6 1</c>.
/// </summary>
public sealed class SequenceReaderService : ISequenceReaderService
{
    private const string FileName = "gatesequence.txt";

    public ReadResult<IReadOnlyList<IReadOnlyList<int>>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<IReadOnlyList<IReadOnlyList<int>>>(path, Parse(File.ReadLines(path)));
    }

    private static readonly char[] Separators = ['[', ']', ',', ' ', '\t'];

    public static IReadOnlyList<IReadOnlyList<int>> Parse(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var sequences = new List<IReadOnlyList<int>>();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            IReadOnlyList<int> numbers = ParseLine(line);
            if (numbers.Count > 0)
                sequences.Add(numbers);
        }

        return sequences;
    }

    private static int[] ParseLine(string line)
    {
        string[] tokens = line.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
        var numbers = new int[tokens.Length];
        for (int i = 0; i < tokens.Length; i++)
        {
            if (!int.TryParse(tokens[i], out numbers[i]))
                throw new FormatException($"'{tokens[i]}' is not a valid integer.");
        }
        return numbers;
    }
}
