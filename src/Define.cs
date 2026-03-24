using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Orbit;

public static class Define
{
    public static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        Converters = { new JsonStringEnumConverter() }
    };

    private const string RootDirName =
#if DEBUG
        ".orbit-debug";
#else
        ".orbit";
#endif

    public static string RootPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), RootDirName);

    public static string ConfigPath => Path.Combine(RootPath, "config.json");

    public static string WorkspacesPath => Path.Combine(RootPath, "workspaces");

    public static string WorkspacePath(string name) => Path.Combine(WorkspacesPath, name);

    public static string WorkspaceConfigPath(string name) => Path.Combine(WorkspacePath(name), "workspace.json");

    public static string TasksPath(string name) => Path.Combine(WorkspacePath(name), "tasks");

    public static string TaskFilePath(string workspaceName, int taskId) =>
        Path.Combine(TasksPath(workspaceName), $"{taskId}.json");

    public static string DescriptionFilePath(string workspaceName, int taskId) =>
        Path.Combine(TasksPath(workspaceName), $"{taskId}.desc.md");
}