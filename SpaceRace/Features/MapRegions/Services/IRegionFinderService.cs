using SpaceRace.Features.MapRegions.Models;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>
/// Finds the connected regions of occupied cells (value 1) in a map, where cells are
/// connected horizontally, vertically, or diagonally (8-directional).
/// </summary>
public interface IRegionFinderService
{
    RegionReport FindRegions(MapGrid grid);
}
