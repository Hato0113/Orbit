using ConsoleAppFramework;

namespace Orbit;

public class Workspace
{
    /// <summary>現在のワークスペース名を表示</summary>
    [Command("")]
    public void Show()
    {
        Console.WriteLine("current workspace");
    }

    /// <summary>ワークスペースを切り替え</summary>
    [Command("switch")]
    public void Switch(string name)
    {
        Console.WriteLine($"switch to: {name}");
    }

    /// <summary>ワークスペースを作成</summary>
    [Command("add")]
    public void Add(string name)
    {
        Console.WriteLine($"add workspace: {name}");
    }

    /// <summary>ワークスペース一覧を表示</summary>
    [Command("list")]
    public void List()
    {
        Console.WriteLine("workspace list");
    }

    /// <summary>ワークスペースを削除</summary>
    [Command("remove")]
    public void Remove(string name)
    {
        Console.WriteLine($"remove workspace: {name}");
    }
}
