namespace SpaceRace.Application;

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
        _functions = functions.ToList();

        if (_functions.Count == 0)
            throw new ArgumentException("At least one function must be registered.", nameof(functions));
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();

            string? choice = Console.ReadLine()?.Trim();

            // Null means end-of-input (e.g. piped/redirected stdin exhausted): exit cleanly.
            if (choice is null || choice == "0")
            {
                Console.WriteLine("Goodbye.");
                return;
            }

            if (int.TryParse(choice, out int selection) &&
                selection >= 1 && selection <= _functions.Count)
            {
                RunFunction(_functions[selection - 1]);
            }
            else
            {
                Console.WriteLine($"\nInvalid selection: '{choice}'. Please enter a number from the menu.\n");
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine($"=== {Title} ===");
        for (int i = 0; i < _functions.Count; i++)
            Console.WriteLine($"{i + 1}. {_functions[i].Title}");
        Console.WriteLine("0. Exit");
        Console.Write("Select an option: ");
    }

    private static void RunFunction(IAppFunction function)
    {
        Console.WriteLine($"\n--- {function.Title} ---");
        try
        {
            function.Run();
        }
        catch (Exception ex) when (ex is FormatException
                                      or ArgumentException
                                      or ArgumentOutOfRangeException
                                      or FileNotFoundException)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
    }
}
