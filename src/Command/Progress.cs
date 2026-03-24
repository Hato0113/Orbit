using ConsoleAppFramework;

namespace Orbit;

public class Progress
{
    /// <summary>タスクに進行ログを追加する (例: obt p 1 "設計完了")</summary>
    /// <param name="id">タスクID</param>
    /// <param name="message">進行ログのメッセージ</param>
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
