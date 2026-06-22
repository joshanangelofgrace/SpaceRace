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
    public ReadResult<IReadOnlyList<DialSet>> Read()
    {
        List<string> lines = ReadStdinLines();
        string source = lines.Count == 0 ? "no input provided" : "standard input";

        return new ReadResult<IReadOnlyList<DialSet>>(source, DialSetParserService.Parse(lines));
    }

    private static List<string> ReadStdinLines()
    {
        var lines = new List<string>();

        if (Console.IsInputRedirected)
        {
            string? line;
            while ((line = Console.ReadLine()) != null)
                lines.Add(line);
        }

        return lines;
    }
}
