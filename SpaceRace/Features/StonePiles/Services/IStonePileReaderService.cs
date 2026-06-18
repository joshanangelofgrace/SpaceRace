using SpaceRace.Features.StonePiles.Models;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>Supplies the stone piles to be evaluated.</summary>
public interface IStonePileReaderService
{
    IReadOnlyList<StonePile> Read();
}
