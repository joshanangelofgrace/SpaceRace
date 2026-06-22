using SpaceRace.Features.SymbolDependencies.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>Supplies the dependency lines to be evaluated, with a description of their source.</summary>
public interface IDependencyReaderService
{
    ReadResult<IReadOnlyList<IReadOnlyList<Dependency>>> Read();
}
