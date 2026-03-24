using ConsoleAppFramework;

namespace Orbit;

public class Workspace
{
    /// <summary>現在のワークスペース名を表示する</summary>
    [Command("")]
    public void Show()
    {
        if (!OrbitHelper.EnsureInitialized()) return;

        var config = OrbitHelper.LoadConfig();
        Console.WriteLine(config.CurrentWorkspace);
    }

    /// <summary>ワークスペースを切り替える (例: obt ws switch my-project)</summary>
    /// <param name="name">切り替え先のワークスペース名</param>
    [Command("switch")]
    public void Switch([Argument] string name)
    {
        if (!OrbitHelper.EnsureInitialized()) return;
        if (!OrbitHelper.ValidateWorkspaceName(name)) return;

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

    /// <summary>新しいワークスペースを作成する (例: obt ws add my-project)</summary>
    /// <param name="name">作成するワークスペース名</param>
    [Command("add")]
    public void Add([Argument] string name)
    {
        if (!OrbitHelper.EnsureInitialized()) return;
        if (!OrbitHelper.ValidateWorkspaceName(name)) return;

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

    /// <summary>ワークスペース一覧を表示する (* が現在のws)</summary>
    [Command("list")]
    public void List()
    {
        if (!OrbitHelper.EnsureInitialized()) return;

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

    /// <summary>ワークスペースを削除する (現在のwsは削除不可)</summary>
    /// <param name="name">削除するワークスペース名</param>
    [Command("remove")]
    public void Remove([Argument] string name)
    {
        if (!OrbitHelper.EnsureInitialized()) return;

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

        Console.WriteLine($"ワークスペース '{name}' を削除します。よろしいですか？ (y/n)");
        var input = Console.ReadLine();
        if (input?.ToLower() != "y")
        {
            Console.WriteLine("キャンセルしました");
            return;
        }

        Directory.Delete(wsPath, true);
        Console.WriteLine($"ワークスペース '{name}' を削除しました");
    }
}
