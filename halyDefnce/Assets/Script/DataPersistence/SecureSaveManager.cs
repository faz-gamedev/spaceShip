using System.IO;
using UnityEngine;

public static class SecureSaveManager
{
    private static string savePath => Path.Combine(Application.persistentDataPath, "savefile.sav");

    public static void SaveGame(SaveData data)
    {
        data.lastSaveTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string json = JsonUtility.ToJson(data);
        string encrypted = EncryptionUtility.Encrypt(json);
        File.WriteAllText(savePath, encrypted);
        Debug.Log(" Game saved (encrypted)).");
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log(" Save file not found. Reverting to default data.");
            return new SaveData();
        }

        try
        {
            string encrypted = File.ReadAllText(savePath);
            string json = EncryptionUtility.Decrypt(encrypted);
            return JsonUtility.FromJson<SaveData>(json);
        }
        catch
        {
            Debug.LogError(" Error loading or decoding. New data will be returned..");
            return new SaveData();
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save file deleted.");
        }
    }
}
