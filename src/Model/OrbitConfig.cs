namespace Orbit;

public readonly record struct OrbitConfig()
{
    public string CurrentWorkspace { get; init; } = "default";
}
