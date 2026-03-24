using ConsoleAppFramework;

namespace Orbit;

public class Clean
{
    /// <summary>~/.orbit を完全に削除する (全データが失われます)</summary>
    [Command("clean")]
    public void Execute()
    {
        var root = new DirectoryInfo(Define.RootPath);
        if (!root.Exists)
        {
            Console.WriteLine("削除対象がありません");
            return;
        }

        Console.WriteLine($"{Define.RootPath} を完全に削除します。よろしいですか？ (y/n)");
        var input = Console.ReadLine();
        if (input?.ToLower() != "y")
        {
            Console.WriteLine("キャンセルしました");
            return;
        }

        root.Delete(true);
        Console.WriteLine("削除しました");
    }
}
