namespace SpaceRace.Features.GateSequence.Services;

/// <summary>
/// Parses raw text lines into integer sequences. Each non-blank line is one sequence;
/// numbers may be separated by commas and/or whitespace and the line may optionally be
/// wrapped in square brackets (e.g. <c>[5, 2, 6, 1]</c> or <c>5 2 6 1</c>).
/// </summary>
public static class SequenceParserService
{
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

    private static IReadOnlyList<int> ParseLine(string line)
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
