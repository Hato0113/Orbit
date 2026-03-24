using ConsoleAppFramework;

namespace Orbit;

public class Progress
{
    /// <summary>進行ログを追加</summary>
    [Command("progress|prg|p")]
    public void Add([Argument] string id, [Argument] string? message = null)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine("メッセージを入力してください: obt p <id> \"message\"");
            return;
        }

        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        var entry = new ProgressEntry
        {
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm"),
            Message = message
        };

        OrbitHelper.SaveJson(path, task with { Progress = [..task.Progress, entry] });
        Console.WriteLine($"#{task.Id}: {message}");
    }
}
