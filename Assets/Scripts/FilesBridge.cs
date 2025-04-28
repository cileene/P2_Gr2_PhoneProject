#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
static class FilesBridge
{
    [DllImport("__Internal")] static extern void pick();
    [DllImport("__Internal")] static extern void shareFile(string path);

    public static void Pick()                => pick();
    public static void Share(string fullPath) => shareFile(fullPath);

    [DllImport("__Internal")] static extern void previewFile(string path);

    public static void Preview(string path)   => previewFile(path);
}
#endif