using ConsoleAppFramework;

namespace Orbit;

public class TaskRemove
{
    /// <summary>指定したタスクを削除する (例: obt remove 1)</summary>
    /// <param name="id">削除するタスクのID</param>
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
