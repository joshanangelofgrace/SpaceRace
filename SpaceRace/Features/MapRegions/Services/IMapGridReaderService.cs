using SpaceRace.Features.MapRegions.Models;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>Supplies the map to be analysed.</summary>
public interface IMapGridReaderService
{
    MapGrid Read();
}
