using SpaceRace.Features.MapRegions.Models;
using SpaceRace.Features.MapRegions.Services;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.MapRegions.Functions;

/// <summary>
/// Menu function for the map puzzle: reads the grid, finds the connected regions of
/// occupied cells (8-directional connectivity) and reports the size of the largest.
/// </summary>
public sealed class LargestRegionFunction(IMapGridReaderService reader, IRegionFinderService service) : IAppFunction
{
    private readonly IMapGridReaderService _reader = reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IRegionFinderService _service = service ?? throw new ArgumentNullException(nameof(service));

    public string Title => "Find largest connected map region";

    public void Run()
    {
        ReadResult<MapGrid> input = _reader.Read();
        Console.WriteLine($"Reading map from: {input.Source}\n");
        MapGrid grid = input.Data;
        RegionReport report = _service.FindRegions(grid);

        Console.WriteLine($"Map: {grid.RowCount} row(s), {report.RegionSizes.Count} region(s) found.");
        if (report.RegionSizes.Count > 0)
            Console.WriteLine($"Region sizes: {string.Join(", ", report.RegionSizes.OrderByDescending(s => s))}");

        Console.WriteLine();
        Console.WriteLine($"Final result (largest region): {report.LargestRegionSize}");
    }
}
