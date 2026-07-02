using SpaceRace.Features.EngineDialTicks.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>
/// Reads dial sets from standard input as pairs of whitespace-separated number lines
/// (current readings followed by expected readings). Blank lines are ignored. When no
/// input is redirected, the worked example from the mission brief is used instead.
/// </summary>
public sealed class DialSetReaderService : IDialSetReaderService
{
    private const string FileName = "dialticks.txt";

    public ReadResult<List<DialSet>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<List<DialSet>>(path, ParseLine(File.ReadLines(path)));
    }

    private static List<DialSet> ParseLine(IEnumerable<string> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        List<string> nonBlank = [.. lines.Where(line => !string.IsNullOrWhiteSpace(line))];

        if (nonBlank.Count % 2 != 0)
            throw new FormatException(
                "Expected an even number of non-blank lines (a current/expected pair per dial set).");

        var sets = new List<DialSet>(nonBlank.Count / 2);
        for (int i = 0; i < nonBlank.Count; i += 2)
        {
            List<Dial> current = ParseLine(nonBlank[i]);
            List<Dial> expected = ParseLine(nonBlank[i + 1]);
            sets.Add(new DialSet(current, expected));
        }

        return sets;
    }

    private static List<Dial> ParseLine(string line) =>
        [.. line.Select(token => new Dial(int.Parse(token.ToString())))];
}
