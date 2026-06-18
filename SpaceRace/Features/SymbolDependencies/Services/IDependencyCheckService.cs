using SpaceRace.Features.SymbolDependencies.Models;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>
/// Decides whether every symbol can be pressed while respecting its dependencies — i.e.
/// whether the dependency graph can be topologically ordered (has no cycle).
/// </summary>
public interface IDependencyCheckService
{
    /// <summary>True when a valid press order exists for the given dependencies.</summary>
    bool IsFeasible(IReadOnlyList<Dependency> dependencies);

    /// <summary>Evaluates every line and totals the per-line results (1 = feasible).</summary>
    DependencyReport Evaluate(IEnumerable<IReadOnlyList<Dependency>> lines);
}
