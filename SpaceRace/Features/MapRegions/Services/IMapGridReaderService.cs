using SpaceRace.Features.MapRegions.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.MapRegions.Services;

/// <summary>Supplies the map to be analysed, with a description of its source.</summary>
public interface IMapGridReaderService
{
    ReadResult<MapGrid> Read();
}
