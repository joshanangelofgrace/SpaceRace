namespace SpaceRace.Infrastructure;

/// <summary>
/// Central error-handling policy for menu functions. Every failure is caught so that one
/// misbehaving function never ends the session: expected input/validation problems are
/// reported as a friendly one-line message, while anything else is surfaced as an
/// unexpected error (with its type) so genuine bugs stay visible.
///
/// This is the single place that decides which exceptions are "the user's input was bad"
/// versus "the program is broken" — keep that classification here rather than scattering
/// try/catch blocks across functions and services.
/// </summary>
public static class FunctionErrorHandler
{
    /// <summary>
    /// Runs <paramref name="action"/>, reporting any failure instead of letting it end the
    /// session. Returns <c>true</c> when the action completed without error, <c>false</c>
    /// when an error was reported.
    /// </summary>
    public static bool Run(Action action)
    {
        ArgumentNullException.ThrowIfNull(action);

        try
        {
            action();
            return true;
        }
        catch (Exception ex)
        {
            if (IsExpectedInputError(ex))
                ConsoleTheme.WriteLine($"Error: {ex.Message}", ConsoleTheme.Error);
            else
                ConsoleTheme.WriteLine($"Unexpected error ({ex.GetType().Name}): {ex.Message}", ConsoleTheme.Error);

            return false;
        }
    }

    /// <summary>
    /// Whether <paramref name="ex"/> represents a problem with the user's input (which we
    /// report and recover from) rather than a programming error (which we let crash).
    /// </summary>
    private static bool IsExpectedInputError(Exception ex) =>
        ex is FormatException
           or ArgumentException          // also covers ArgumentOutOfRangeException / ArgumentNullException
           or FileNotFoundException;
}
