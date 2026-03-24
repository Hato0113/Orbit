using System.Text.Json;

namespace Orbit;

public static class OrbitHelper
{
    public static T LoadJson<T>(string path)
    {
        return JsonSerializer.Deserialize<T>(File.ReadAllText(path), Define.JsonOptions)!;
    }

    public static void SaveJson<T>(string path, T value)
    {
        File.WriteAllText(path, JsonSerializer.Serialize(value, Define.JsonOptions));
    }

    public static bool EnsureInitialized()
    {
        if (Directory.Exists(Define.RootPath)) return true;

        Console.WriteLine("Orbit が初期化されていません。先に `obt init` を実行してください");
        return false;
    }

    public static bool TryParseTaskId(string id, out int taskId)
    {
        if (int.TryParse(id, out taskId) && taskId > 0) return true;

        Console.WriteLine($"'{id}' は有効なタスクIDではありません (1以上の整数)");
        return false;
    }

    private static readonly char[] InvalidNameChars = [' ', '/', '\\', ':', '*', '?', '"', '<', '>', '|', '.'];

    public static bool ValidateWorkspaceName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("ワークスペース名を指定してください");
            return false;
        }

        if (name.IndexOfAny(InvalidNameChars) >= 0)
        {
            Console.WriteLine($"ワークスペース名に使用できない文字が含まれています: スペース / \\ : * ? \" < > | .");
            return false;
        }

        return true;
    }

    public static OrbitConfig LoadConfig()
    {
        return LoadJson<OrbitConfig>(Define.ConfigPath);
    }

    public static (OrbitTask Task, string? Path) LoadTask(string id)
    {
        if (!EnsureInitialized()) return (default, null);
        if (!TryParseTaskId(id, out var taskId)) return (default, null);

        var wsName = LoadConfig().CurrentWorkspace;
        var path = Define.TaskFilePath(wsName, taskId);

        if (!File.Exists(path))
        {
            Console.WriteLine($"タスク #{taskId} が見つかりません");
            return (default, null);
        }

        return (LoadJson<OrbitTask>(path), path);
    }
}
