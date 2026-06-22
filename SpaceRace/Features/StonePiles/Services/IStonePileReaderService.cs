using SpaceRace.Features.StonePiles.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.StonePiles.Services;

/// <summary>Supplies the stone piles to be evaluated, with a description of their source.</summary>
public interface IStonePileReaderService
{
    ReadResult<IReadOnlyList<StonePile>> Read();
}
