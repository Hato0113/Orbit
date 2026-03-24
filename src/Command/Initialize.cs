using ConsoleAppFramework;

namespace Orbit;

public class Initialize
{
    /// <summary>Orbit を初期化する (~/.orbit を作成し、デフォルトワークスペースを準備)</summary>
    [Command("init")]
    public void Init()
    {
        var root = new DirectoryInfo(Define.RootPath);
        if (root.Exists)
        {
            Console.WriteLine($"すでにディレクトリが存在します {Define.RootPath}\n初期化しますか？ (y/n)");
            var input = Console.ReadLine();
            if (input?.ToLower() != "y")
            {
                Console.WriteLine("初期化をキャンセルしました");
                return;
            }

            root.Delete(true);
        }

        root.Create();

        var config = new OrbitConfig();
        OrbitHelper.SaveJson(Define.ConfigPath, config);

        var wsName = config.CurrentWorkspace;
        Directory.CreateDirectory(Define.TasksPath(wsName));
        OrbitHelper.SaveJson(Define.WorkspaceConfigPath(wsName), new WorkspaceConfig());

        Console.WriteLine("初期化しました");
    }
}
