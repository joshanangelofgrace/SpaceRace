using SpaceRace.Features.SymbolDependencies.Models;
using SpaceRace.Infrastructure;

namespace SpaceRace.Features.SymbolDependencies.Services;

/// <summary>
/// Reads dependency lines from <c>symboldependencies.txt</c> (shipped with the solution
/// and copied to the output directory). Each non-blank line is one set of bracketed
/// dependency pairs, e.g. <c>[*,&lt;],[&lt;,*]</c>.
/// </summary>
public sealed class DependencyReaderService : IDependencyReaderService
{
    private const string FileName = "symboldependencies.txt";

    public ReadResult<IReadOnlyList<IReadOnlyList<Dependency>>> Read()
    {
        string path = InputFileLocator.FindRequired(FileName);
        return new ReadResult<IReadOnlyList<IReadOnlyList<Dependency>>>(path, DependencyParserService.Parse(File.ReadLines(path)));
    }
}
