using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(SaveData saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Save.save";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, saveData);
        stream.Close();
    }
    public static SaveData Load()
    {
        string path = Application.persistentDataPath + "/Save.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = (SaveData)formatter.Deserialize(stream);
            stream.Close();
            return data;
        }
        else
        {
            SaveSystem.Save(new SaveData());
            Debug.LogError("Save file was create : " + path);
            return null;
        }
    }
}
