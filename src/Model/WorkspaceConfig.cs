namespace Orbit;

public readonly record struct WorkspaceConfig()
{
    public int NextTaskNumber { get; init; } = 1;
}
