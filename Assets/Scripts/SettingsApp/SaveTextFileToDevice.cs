using System.IO;
using UnityEngine;

namespace SettingsApp
{
    public static class SaveTextFileToDevice
    {
        public static void SaveTextFile(string fileName, string text)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            File.WriteAllText(filePath, text);
            Debug.Log($"{fileName} saved to: {filePath}");
        }
    }
}