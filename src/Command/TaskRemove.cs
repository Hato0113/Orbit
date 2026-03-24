using ConsoleAppFramework;

namespace Orbit;

public class TaskRemove
{
    /// <summary>タスクを削除</summary>
    [Command("remove")]
    public void Execute([Argument] string id)
    {
        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        File.Delete(path);

        if (!string.IsNullOrEmpty(task.Description) && File.Exists(task.Description))
        {
            File.Delete(task.Description);
        }

        Console.WriteLine($"#{task.Id}: {task.Title} を削除しました");
    }
}
