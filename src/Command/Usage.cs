using ConsoleAppFramework;

namespace Orbit;

public class Usage
{
    private const string Reset = "\x1b[0m";
    private const string Bold = "\x1b[1m";
    private const string Dim = "\x1b[2m";

    private const string White = "\x1b[37m";
    private const string Cyan = "\x1b[96m";
    private const string Green = "\x1b[92m";
    private const string Red = "\x1b[91m";
    private const string Yellow = "\x1b[93m";

    /// <summary>コマンドの使い方をカラー表示する</summary>
    [Command("usage")]
    public void Execute()
    {
        Console.WriteLine();

        WriteSection("セットアップ", White, [
            ("init", "Orbit を初期化する"),
        ]);

        WriteSection("タスク進行", Cyan, [
            ("add <title>", "新しいタスクを作成する"),
            ("p <id> <message>", "進行ログを追加する (progress, prg)"),
            ("done <id>", "タスクを完了にする"),
        ]);

        WriteSection("タスク管理", Green, [
            ("show [id|+tag]", "タスク一覧 / 詳細  -a: 全件  -v: 経過付き"),
            ("desc <id> [text]", "説明を追記する (省略時はエディタ起動)"),
            ("rename <id> <title>", "タスクのタイトルを変更する"),
            ("tag <id> +tag -tag", "タグを追加/削除する"),
        ]);

        WriteSection("削除", Red, [
            ("remove <id>", "タスクを削除する"),
            ("clean", "全データを削除する (~/.orbit)"),
        ]);

        WriteSection("ワークスペース", Yellow, [
            ("ws", "現在のワークスペースを表示"),
            ("ws list", "ワークスペース一覧を表示"),
            ("ws add <name>", "ワークスペースを作成"),
            ("ws switch <name>", "ワークスペースを切り替え"),
            ("ws remove <name>", "ワークスペースを削除"),
        ]);

        Console.WriteLine($"  {Dim}各コマンドの詳細: obt <command> -h{Reset}");
        Console.WriteLine();
    }

    private static void WriteSection(string title, string color, (string Cmd, string Desc)[] commands)
    {
        Console.WriteLine($"  {Bold}{color}[ {title} ]{Reset}");

        foreach (var (cmd, desc) in commands)
        {
            Console.WriteLine($"    {color}obt {cmd,-24}{Reset} {desc}");
        }

        Console.WriteLine();
    }
}
