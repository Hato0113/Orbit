using ConsoleAppFramework;

namespace Orbit;

public class Description
{
    /// <summary>Descriptionをインライン上書き or エディタで編集</summary>
    [Command("desc")]
    public void Desc([Argument] string id, [Argument] string? text = null)
    {
        if (text is null)
            Console.WriteLine($"open editor for: {id}");
        else
            Console.WriteLine($"set description of {id}: {text}");
    }
}
