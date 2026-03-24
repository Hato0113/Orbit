using ConsoleAppFramework;

namespace Orbit;

public class TaskTag
{
    /// <summary>タスクにタグを追加/削除する (例: obt tag 1 +backend -frontend)</summary>
    /// <param name="id">タスクID</param>
    /// <param name="tags">+tag で追加、-tag で削除</param>
    [Command("tag")]
    public void Execute([Argument] string id, [Argument] params string[] tags)
    {
        if (tags.Length == 0)
        {
            Console.WriteLine("タグを指定してください: obt tag <id> +tag / -tag");
            return;
        }

        var (task, path) = OrbitHelper.LoadTask(id);
        if (path is null) return;

        var currentTags = new List<string>(task.Tags);

        foreach (var tag in tags)
        {
            if (tag.Length <= 1 || (tag[0] != '+' && tag[0] != '-'))
            {
                Console.WriteLine($"'{tag}' は無効な形式です（+tag または -tag）");
                continue;
            }

            var name = tag[1..].ToLowerInvariant();

            if (tag[0] == '+')
            {
                if (!currentTags.Contains(name))
                {
                    currentTags.Add(name);
                }
            }
            else
            {
                currentTags.Remove(name);
            }
        }

        OrbitHelper.SaveJson(path, task with { Tags = currentTags });

        var display = currentTags.Count > 0
            ? string.Join(" ", currentTags.Select(t => $"+{t}"))
            : "(タグなし)";
        Console.WriteLine($"#{task.Id}: {display}");
    }
}
