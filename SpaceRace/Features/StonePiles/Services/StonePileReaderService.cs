using SpaceRace.Features.StonePiles.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>
/// Reads stone piles from <c>stonepiles.txt</c> (shipped with the solution and copied to
/// the output directory). Each non-blank line is a pile size and a bracketed factor set,
/// e.g. <c>12 [3,6,5]</c>.
/// </summary>
public sealed class StonePileReaderService : IStonePileReaderService
{
    private const string FileName = "stonepiles.txt";

    public ReadResult<IReadOnlyList<StonePile>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<IReadOnlyList<StonePile>>(path, StonePileParserService.Parse(File.ReadLines(path)));
    }
}
