using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string directory = "/Save/";
    public static string fileName = "SaveData.txt";

    public static void SaveGame(GameManager manager)
    {
        SaveData data = new SaveData(manager);

        string dir = Application.persistentDataPath + directory;

        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + fileName, json);
    }

    public static SaveData LoadGame()
    {
        string path = Application.persistentDataPath + directory + fileName;
        SaveData data = new SaveData();

        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            Debug.LogError("Save not found");
        }

        return data;
    }
}
