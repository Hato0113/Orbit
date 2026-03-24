namespace Orbit;

public enum TaskStatus
{
    InProgress,
    Done
}

public readonly record struct OrbitTask()
{
    public int Id { get; init; } = 0;
    public string Title { get; init; } = string.Empty;
    public TaskStatus Status { get; init; } = TaskStatus.InProgress;
    public string CreatedAt { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public List<ProgressEntry> Progress { get; init; } = [];
}

public readonly record struct ProgressEntry()
{
    public string Timestamp { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
}
