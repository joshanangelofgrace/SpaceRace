using SpaceRace.Features.SymbolDependencies.Models;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>Supplies the dependency lines to be evaluated.</summary>
public interface IDependencyReaderService
{
    IReadOnlyList<IReadOnlyList<Dependency>> Read();
}
