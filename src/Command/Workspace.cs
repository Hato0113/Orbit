using ConsoleAppFramework;

namespace Orbit;

public class Workspace
{
    /// <summary>現在のワークスペース名を表示</summary>
    [Command("")]
    public void Show()
    {
        var config = OrbitHelper.LoadConfig();
        Console.WriteLine(config.CurrentWorkspace);
    }

    /// <summary>ワークスペースを切り替え</summary>
    [Command("switch")]
    public void Switch([Argument] string name)
    {
        var wsPath = Define.WorkspacePath(name);
        if (!Directory.Exists(wsPath))
        {
            Console.WriteLine($"ワークスペース '{name}' が見つかりません");
            return;
        }

        var config = OrbitHelper.LoadConfig();
        OrbitHelper.SaveJson(Define.ConfigPath, config with { CurrentWorkspace = name });
        Console.WriteLine($"ワークスペースを '{name}' に切り替えました");
    }

    /// <summary>ワークスペースを作成</summary>
    [Command("add")]
    public void Add([Argument] string name)
    {
        var wsPath = Define.WorkspacePath(name);
        if (Directory.Exists(wsPath))
        {
            Console.WriteLine($"ワークスペース '{name}' はすでに存在します");
            return;
        }

        Directory.CreateDirectory(Define.TasksPath(name));
        OrbitHelper.SaveJson(Define.WorkspaceConfigPath(name), new WorkspaceConfig());
        Console.WriteLine($"ワークスペース '{name}' を作成しました");
    }

    /// <summary>ワークスペース一覧を表示</summary>
    [Command("list")]
    public void List()
    {
        var wsDir = Define.WorkspacesPath;
        if (!Directory.Exists(wsDir))
        {
            Console.WriteLine("ワークスペースがありません");
            return;
        }

        var current = OrbitHelper.LoadConfig().CurrentWorkspace;
        var dirs = Directory.GetDirectories(wsDir);

        foreach (var dir in dirs.OrderBy(d => d))
        {
            var name = Path.GetFileName(dir);
            var taskCount = Directory.Exists(Define.TasksPath(name))
                ? Directory.GetFiles(Define.TasksPath(name), "*.json").Length
                : 0;
            var marker = name == current ? "*" : " ";
            Console.WriteLine($"  {marker} {name}  ({taskCount} tasks)");
        }
    }

    /// <summary>ワークスペースを削除</summary>
    [Command("remove")]
    public void Remove([Argument] string name)
    {
        var current = OrbitHelper.LoadConfig().CurrentWorkspace;
        if (name == current)
        {
            Console.WriteLine("現在のワークスペースは削除できません");
            return;
        }

        var wsPath = Define.WorkspacePath(name);
        if (!Directory.Exists(wsPath))
        {
            Console.WriteLine($"ワークスペース '{name}' が見つかりません");
            return;
        }

        Directory.Delete(wsPath, true);
        Console.WriteLine($"ワークスペース '{name}' を削除しました");
    }
}
