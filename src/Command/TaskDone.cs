using ConsoleAppFramework;

namespace Orbit;

public class TaskDone
{
    /// <summary>指定したタスクを完了にする (例: obt done 1)</summary>
    /// <param name="id">完了にするタスクのID</param>
    [Command("done")]
    public void Execute([Argument] string id)
    {
        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        OrbitHelper.SaveJson(path, task with { Status = TaskStatus.Done });
        Console.WriteLine($"#{task.Id}: {task.Title} を完了にしました");
    }
}
