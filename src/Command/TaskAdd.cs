using ConsoleAppFramework;

namespace Orbit;

public class TaskAdd
{
    /// <summary>新しいタスクを作成する (例: obt add "認証機能の実装" +backend +api)</summary>
    /// <param name="title">タスクのタイトル</param>
    /// <param name="options">+tag でタグを付与</param>
    [Command("add")]
    public void Execute([Argument] string title, [Argument] params string[] options)
    {
        if (!OrbitHelper.EnsureInitialized()) return;

        var wsName = OrbitHelper.LoadConfig().CurrentWorkspace;

        var wsConfigPath = Define.WorkspaceConfigPath(wsName);
        var wsConfig = OrbitHelper.LoadJson<WorkspaceConfig>(wsConfigPath);

        var tags = options
            .Where(o => o.StartsWith('+') && o.Length > 1)
            .Select(o => o[1..].ToLowerInvariant())
            .Distinct()
            .ToList();

        var taskId = wsConfig.NextTaskNumber;
        var descPath = Define.DescriptionFilePath(wsName, taskId);
        var task = new OrbitTask
        {
            Id = taskId,
            Title = title,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
            Description = descPath,
            Tags = tags
        };

        OrbitHelper.SaveJson(Define.TaskFilePath(wsName, taskId), task);
        File.WriteAllText(descPath, string.Empty);
        OrbitHelper.SaveJson(wsConfigPath, wsConfig with { NextTaskNumber = wsConfig.NextTaskNumber + 1 });

        var tagDisplay = tags.Count > 0 ? "  " + string.Join(" ", tags.Select(t => $"+{t}")) : string.Empty;
        Console.WriteLine($"#{taskId}: {title}{tagDisplay}");
    }
}
