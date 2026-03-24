using ConsoleAppFramework;

namespace Orbit;

public class TaskAdd
{
    /// <summary>タスクを作成</summary>
    [Command("add")]
    public void Execute([Argument] string title)
    {
        var wsName = OrbitHelper.LoadConfig().CurrentWorkspace;

        var wsConfigPath = Define.WorkspaceConfigPath(wsName);
        var wsConfig = OrbitHelper.LoadJson<WorkspaceConfig>(wsConfigPath);

        var taskId = wsConfig.NextTaskNumber;
        var descPath = Define.DescriptionFilePath(wsName, taskId);
        var task = new OrbitTask
        {
            Id = taskId,
            Title = title,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd"),
            Description = descPath
        };

        OrbitHelper.SaveJson(Define.TaskFilePath(wsName, taskId), task);
        File.WriteAllText(descPath, string.Empty);
        OrbitHelper.SaveJson(wsConfigPath, wsConfig with { NextTaskNumber = wsConfig.NextTaskNumber + 1 });

        Console.WriteLine($"#{taskId}: {title}");
    }
}
