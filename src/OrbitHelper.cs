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

    public static OrbitConfig LoadConfig()
    {
        return LoadJson<OrbitConfig>(Define.ConfigPath);
    }

    public static (OrbitTask Task, string? Path) LoadTask(string id)
    {
        var wsName = LoadConfig().CurrentWorkspace;
        var taskId = int.Parse(id);
        var path = Define.TaskFilePath(wsName, taskId);

        if (!File.Exists(path))
        {
            Console.WriteLine($"タスク #{taskId} が見つかりません");
            return (default, null);
        }

        return (LoadJson<OrbitTask>(path), path);
    }
}
