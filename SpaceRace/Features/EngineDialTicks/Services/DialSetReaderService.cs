using SpaceRace.Features.EngineDialTicks.Models;

namespace SpaceRace.Features.EngineDialTicks.Services;

/// <summary>
/// Reads dial sets from standard input as pairs of whitespace-separated number lines
/// (current readings followed by expected readings). Blank lines are ignored. When no
/// input is redirected, the worked example from the mission brief is used instead.
/// </summary>
public sealed class DialSetReaderService : IDialSetReaderService
{
    public IReadOnlyList<DialSet> Read()
    {
        List<string> lines = ReadStdinLines();

        if (lines.Count == 0)
        {
            Console.WriteLine("No input provided\n");
        }

        return DialSetParserService.Parse(lines);
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
