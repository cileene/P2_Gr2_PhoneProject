using System.IO;
using UnityEngine;

public class SaveTextFileToDevice : MonoBehaviour
{
    private void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "theTruth.txt");
        File.WriteAllText(filePath, "It was you!! You did it!! It's your phone!!!!");
    }
}