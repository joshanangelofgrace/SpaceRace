using SpaceRace.Features.MapRegions.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>
/// Reads the map from <c>mapregions.txt</c> (shipped with the solution and copied to the
/// output directory). Each non-blank line is one row of cell values, e.g. <c>1 1 0 0</c>.
/// </summary>
public sealed class MapGridReaderService : IMapGridReaderService
{
    private const string FileName = "mapregions.txt";

    public ReadResult<MapGrid> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<MapGrid>(path, MapParserService.Parse(File.ReadLines(path)));
    }
}
