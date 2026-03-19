using ConsoleAppFramework;

namespace Orbit;

public class TaskCommand
{
    /// <summary>タスクを作成</summary>
    [Command("add")]
    public void Add(string title)
    {
        Console.WriteLine($"add task: {title}");
    }

    /// <summary>タスク一覧 or 詳細を表示</summary>
    [Command("show")]
    public void Show([Argument] string? id = null)
    {
        if (id is null)
            Console.WriteLine("show all tasks");
        else
            Console.WriteLine($"show task: {id}");
    }

    /// <summary>タスクを完了にする</summary>
    [Command("done")]
    public void Done([Argument] string id)
    {
        Console.WriteLine($"done: {id}");
    }

    /// <summary>タスクを削除</summary>
    [Command("remove")]
    public void Remove([Argument] string id)
    {
        Console.WriteLine($"remove: {id}");
    }
}
