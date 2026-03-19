using ConsoleAppFramework;

namespace Orbit;

public class Progress
{
    /// <summary>進行ログをインライン追加 or エディタで追加</summary>
    [Command("progress")]
    public void Add([Argument] string id, [Argument] string? message = null)
    {
        if (message is null)
            Console.WriteLine($"open editor for: {id}");
        else
            Console.WriteLine($"add progress to {id}: {message}");
    }
}
