using UnityEngine;
using System.IO;

public static class SaveDataManager
{
    public static void TriggerSave()
    {
        if (GameManager.Instance.useSaveData) WriteSaveData();
    }

    private static void WriteSaveData()
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
        string json = JsonUtility.ToJson(GameManager.Instance, true);
        if (GameManager.Instance.obfuscateData)
        {
            byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
            return System.Convert.ToBase64String(jsonBytes);
        }
        return json;
    }

    private static void DeserializeFromJson(string data)
    {
        string json;
        if (GameManager.Instance.obfuscateData)
            json = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(data));
        else
            json = data;

        JsonUtility.FromJsonOverwrite(json, GameManager.Instance);
    }
}