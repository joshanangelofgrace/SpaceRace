namespace SpaceRace.Application;

/// <summary>
/// Menu function that reads the dial sets and reports each set's ticks plus the final
/// product. Adapts <see cref="EngineAdjustmentApp"/> to the <see cref="IAppFunction"/>
/// menu contract.
/// </summary>
public sealed class TickProcessingFunction : IAppFunction
{
    private readonly EngineAdjustmentApp _app;

    public TickProcessingFunction(EngineAdjustmentApp app)
    {
        _app = app ?? throw new ArgumentNullException(nameof(app));
    }

    public string Title => "Process engine dial ticks";

    public void Run() => _app.Run();
}
