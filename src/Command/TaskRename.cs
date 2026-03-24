using ConsoleAppFramework;

namespace Orbit;

public class TaskRename
{
    /// <summary>タスクのタイトルを変更する (例: obt rename 1 "新しいタイトル")</summary>
    /// <param name="id">タスクID</param>
    /// <param name="title">新しいタイトル</param>
    [Command("rename")]
    public void Execute([Argument] string id, [Argument] string title)
    {
        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        OrbitHelper.SaveJson(path, task with { Title = title });
        Console.WriteLine($"#{task.Id}: {task.Title} → {title}");
    }
}
