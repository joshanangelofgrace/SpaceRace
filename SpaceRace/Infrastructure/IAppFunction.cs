namespace SpaceRace.Infrastructure;

/// <summary>
/// One selectable function in the terminal menu. Implement this interface and register
/// the implementation in the DI container (see <c>Program.cs</c>) to add a new menu
/// item — the menu lists every registered function automatically.
/// </summary>
public interface IAppFunction
{
    /// <summary>Label shown in the menu.</summary>
    string Title { get; }

    /// <summary>Runs the function. Thrown exceptions are reported by the menu, which
    /// then returns to the prompt, so a single failing function never ends the session.</summary>
    void Run();
}
