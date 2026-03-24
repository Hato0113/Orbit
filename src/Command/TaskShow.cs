using ConsoleAppFramework;

namespace Orbit;

public class TaskShow
{
    /// <summary>タスク一覧を表示、またはIDを指定して詳細を表示する</summary>
    /// <param name="id">タスクID (省略時は一覧表示)</param>
    /// <param name="all">-a, 完了済みタスクも含めて表示</param>
    /// <param name="verbose">-v, 各タスクの最新の進行ログも表示</param>
    [Command("show")]
    public void Execute([Argument] string? id = null, bool all = false, bool verbose = false)
    {
        if (!OrbitHelper.EnsureInitialized()) return;

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
        else if (id.StartsWith('+') && id.Length > 1)
        {
            var tagFilter = id[1..].ToLowerInvariant();
            ShowList(tasksDir, all, verbose, tagFilter);
        }
        else
        {
            if (!OrbitHelper.TryParseTaskId(id, out var taskId)) return;
            ShowDetail(wsName, taskId);
        }
    }

    private static void ShowList(string tasksDir, bool showAll, bool verbose, string? tagFilter = null)
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

        IEnumerable<OrbitTask> filtered = showAll ? tasks : tasks.Where(t => t.Status != TaskStatus.Done);

        if (tagFilter is not null)
        {
            filtered = filtered.Where(t => t.Tags.Contains(tagFilter));
        }

        var hasOutput = false;
        foreach (var task in filtered)
        {
            var mark = task.Status == TaskStatus.Done ? "x" : " ";
            var tagDisplay = task.Tags.Count > 0 ? "  " + string.Join(" ", task.Tags.Select(t => $"+{t}")) : string.Empty;
            Console.WriteLine($"  #{task.Id}  [{mark}]  {task.Title}{tagDisplay}");

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

        if (task.Tags.Count > 0)
        {
            Console.WriteLine($"Tags:    {string.Join(", ", task.Tags)}");
        }

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
