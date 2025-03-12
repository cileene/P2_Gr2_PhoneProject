using UnityEngine;
using System.IO;

public static class SaveDataManager
{
    public static void TriggerSave()
    {
        if (!GameManager.Instance.useSaveData) return;
        WriteSaveData();
    }

    public static void WriteSaveData()
    {
        File.WriteAllText(GameManager.Instance.SaveData, SerializeToJson());
    }

    public static void ReadSaveData(string data)
    {
        if (!File.Exists(data))
        {
            Debug.LogWarning("File does not exist!");
            return;
        }

        string text = File.ReadAllText(data);
        Debug.Log(text);
        DeserializeFromJson(text);
    }


    // JSON serialization methods
    private static string SerializeToJson()
    {
        return JsonUtility.ToJson(GameManager.Instance, true);
    }

    private static void DeserializeFromJson(string data)
    {
        JsonUtility.FromJsonOverwrite(data, GameManager.Instance);
    }
}