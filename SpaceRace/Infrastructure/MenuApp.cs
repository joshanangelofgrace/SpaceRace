namespace SpaceRace.Infrastructure;

/// <summary>
/// The terminal front end: shows a numbered menu of every registered
/// <see cref="IAppFunction"/>, runs the chosen one, then returns to the menu until the
/// user quits. Exceptions thrown by a function are reported and the loop continues.
/// </summary>
public sealed class MenuApp
{
    private const string Title = "USS MM Guide";

    private readonly IReadOnlyList<IAppFunction> _functions;

    public MenuApp(IEnumerable<IAppFunction> functions)
    {
        ArgumentNullException.ThrowIfNull(functions);
        _functions = [.. functions];

        if (_functions.Count == 0)
            throw new ArgumentException("At least one function must be registered.", nameof(functions));
    }

    public void Run()
    {
        ConsoleTheme.Apply();
        ConsoleTheme.WriteBanner();

        while (true)
        {
            ShowMenu();

            string? choice = Console.ReadLine()?.Trim();

            // Null means end-of-input (e.g. piped/redirected stdin exhausted): exit cleanly.
            if (choice is null || choice == "0")
            {
                ConsoleTheme.WriteLine("\nSafe travels, crew. Goodbye.", ConsoleTheme.Highlight);
                return;
            }

            if (int.TryParse(choice, out int selection) &&
                selection >= 1 && selection <= _functions.Count)
            {
                RunFunction(_functions[selection - 1]);
            }
            else
            {
                ConsoleTheme.WriteLine(
                    $"\nInvalid selection: '{choice}'. Please enter a number from the menu.\n",
                    ConsoleTheme.Error);
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine();
        ConsoleTheme.WriteLine($"==={{ {Title} }}===", ConsoleTheme.Highlight);
        for (int i = 0; i < _functions.Count; i++)
        {
            ConsoleTheme.Write($"  [{i + 1}] ", ConsoleTheme.Highlight);
            ConsoleTheme.WriteLine(_functions[i].Title, ConsoleTheme.Primary);
        }
        ConsoleTheme.Write("  [0] ", ConsoleTheme.Highlight);
        ConsoleTheme.WriteLine("Exit", ConsoleTheme.Dim);
        ConsoleTheme.Write("▶ Select an option: ", ConsoleTheme.Highlight);
    }

    private static void RunFunction(IAppFunction function)
    {
        ConsoleTheme.WriteLine($"\n--- {function.Title} ---", ConsoleTheme.Highlight);
        FunctionErrorHandler.Run(function.Run);
        Console.WriteLine();
    }
}
