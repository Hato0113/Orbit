namespace Orbit;

public static class Define
{
    public static string RootPath =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".orbit");
}