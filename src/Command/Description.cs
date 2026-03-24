using System.Diagnostics;
using ConsoleAppFramework;

namespace Orbit;

public class Description
{
    /// <summary>Descriptionをインライン追記 or エディタで編集</summary>
    [Command("desc")]
    public void Edit([Argument] string id, [Argument] string? message = null)
    {
        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        if (string.IsNullOrEmpty(task.Description) || !File.Exists(task.Description))
        {
            Console.WriteLine($"タスク #{task.Id} の Description ファイルが見つかりません");
            return;
        }

        if (message is not null)
        {
            File.AppendAllText(task.Description, message + Environment.NewLine);
            Console.WriteLine($"#{task.Id}: Description に追記しました");
            return;
        }

        var editor = Environment.GetEnvironmentVariable("EDITOR") ?? "vi";
        var process = Process.Start(new ProcessStartInfo
        {
            FileName = editor,
            Arguments = task.Description,
            UseShellExecute = true
        });

        process?.WaitForExit();
        Console.WriteLine($"#{task.Id}: Description を更新しました");
    }
}
