using ConsoleAppFramework;

namespace Orbit;

public class TaskShow
{
    /// <summary>タスク一覧 or 詳細を表示</summary>
    /// <param name="all">-a, Doneも含めて表示</param>
    /// <param name="verbose">-v, 最新の経過も表示</param>
    [Command("show")]
    public void Execute([Argument] string? id = null, bool all = false, bool verbose = false)
    {
        var wsName = OrbitHelper.LoadConfig().CurrentWorkspace;
        var tasksDir = Define.TasksPath(wsName);

        if (!Directory.Exists(tasksDir))
        {
            Console.WriteLine("タスクがありません");
            return;
        }

        if (id is null)
        {
            ShowList(tasksDir, all, verbose);
        }
        else
        {
            ShowDetail(wsName, int.Parse(id));
        }
    }

    private static void ShowList(string tasksDir, bool showAll, bool verbose)
    {
        var files = Directory.GetFiles(tasksDir, "*.json");
        if (files.Length == 0)
        {
            Console.WriteLine("タスクがありません");
            return;
        }

        var tasks = files
            .Select(f => OrbitHelper.LoadJson<OrbitTask>(f))
            .OrderBy(t => t.Id);

        var filtered = showAll ? tasks : tasks.Where(t => t.Status != TaskStatus.Done);

        var hasOutput = false;
        foreach (var task in filtered)
        {
            var mark = task.Status == TaskStatus.Done ? "x" : " ";
            Console.WriteLine($"  #{task.Id}  [{mark}]  {task.Title}");

            if (verbose && task.Progress.Count > 0)
            {
                var latest = task.Progress[^1];
                Console.WriteLine($"           └ [{latest.Timestamp}] {latest.Message}");
            }

            hasOutput = true;
        }

        if (!hasOutput)
        {
            Console.WriteLine("タスクがありません");
        }
    }

    private static void ShowDetail(string wsName, int taskId)
    {
        var path = Define.TaskFilePath(wsName, taskId);
        if (!File.Exists(path))
        {
            Console.WriteLine($"タスク #{taskId} が見つかりません");
            return;
        }

        var task = OrbitHelper.LoadJson<OrbitTask>(path);

        Console.WriteLine($"─── #{task.Id}: {task.Title} ──────────────");
        Console.WriteLine($"Status:  {task.Status}");
        Console.WriteLine($"Created: {task.CreatedAt}");

        if (!string.IsNullOrEmpty(task.Description) && File.Exists(task.Description))
        {
            var descContent = File.ReadAllText(task.Description).Trim();
            if (!string.IsNullOrEmpty(descContent))
            {
                Console.WriteLine();
                Console.WriteLine("Description:");
                foreach (var line in descContent.Split('\n'))
                {
                    Console.WriteLine($"  {line}");
                }
            }
        }

        if (task.Progress.Count > 0)
        {
            Console.WriteLine();
            Console.WriteLine("Progress:");
            foreach (var entry in task.Progress)
            {
                Console.WriteLine($"  [{entry.Timestamp}] {entry.Message}");
            }
        }
    }
}
