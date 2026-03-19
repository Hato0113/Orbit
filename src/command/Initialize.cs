using ConsoleAppFramework;

namespace Orbit;

public class Initialize
{
    /// <summary>
    /// ディレクトリを初期化します
    /// </summary>
    [Command("init")]
    public void Init()
    {
        var root = new DirectoryInfo(Define.RootPath);
        if (root.Exists)
        {
            // 存在するなら、初期化しますか？
            Console.WriteLine($"すでにディレクトリが存在します {Define.RootPath}\n初期化しますか？ (y/n)");
            var input = Console.ReadLine();
            if (input?.ToLower() != "y")
            {
                Console.WriteLine("初期化をキャンセルしました");
                return;
            }

            root.Delete();
        }

        root.Create();
    }
}