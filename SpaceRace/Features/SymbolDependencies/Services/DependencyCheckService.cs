using SpaceRace.Features.SymbolDependencies.Models;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <inheritdoc />
public sealed class DependencyCheckService : IDependencyCheckService
{
    public bool IsFeasible(IReadOnlyList<Dependency> dependencies)
    {
        ArgumentNullException.ThrowIfNull(dependencies);

        // Directed edge: prerequisite -> symbol (the prerequisite must come first).
        var dependents = new Dictionary<char, List<char>>();
        var inDegree = new Dictionary<char, int>();

        void Ensure(char symbol)
        {
            if (!inDegree.ContainsKey(symbol))
            {
                inDegree[symbol] = 0;
                dependents[symbol] = [];
            }
        }

        foreach (Dependency dependency in dependencies)
        {
            Ensure(dependency.Symbol);
            Ensure(dependency.Prerequisite);

            dependents[dependency.Prerequisite].Add(dependency.Symbol);
            inDegree[dependency.Symbol]++;
        }

        // Kahn's algorithm: repeatedly press symbols with no outstanding prerequisites.
        var ready = new Queue<char>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
        int pressed = 0;
        while (ready.Count > 0)
        {
            char symbol = ready.Dequeue();
            pressed++;
            foreach (char dependent in dependents[symbol])
            {
                if (--inDegree[dependent] == 0)
                    ready.Enqueue(dependent);
            }
        }

        // If every symbol was pressed, the graph is acyclic and a valid order exists.
        return pressed == inDegree.Count;
    }

    public DependencyReport Evaluate(IEnumerable<IReadOnlyList<Dependency>> lines)
    {
        ArgumentNullException.ThrowIfNull(lines);

        var checks = new List<DependencyCheck>();
        long sum = 0;
        foreach (IReadOnlyList<Dependency> dependencies in lines)
        {
            bool feasible = IsFeasible(dependencies);
            var check = new DependencyCheck(dependencies, feasible);
            checks.Add(check);
            sum += check.Result;
        }

        if (checks.Count == 0)
            throw new ArgumentException("At least one dependency line is required.", nameof(lines));

        return new DependencyReport(checks, sum);
    }
}
