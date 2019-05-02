using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SoubraSaveLoad
{

    public static void Save(SaveSystem.ObjectSaved objectSavedData)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //Need this to write to a file
        string path = Application.persistentDataPath + "/Save.Soubra";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);
        formatter.Serialize(stream, objectSavedData);
        stream.Close();
        Debug.Log("Scene saved");
    }

    public static void Load(SaveSystem saveFile)
    {
        string file = Application.persistentDataPath + "/Save.Soubra";

        if (File.Exists(file))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(file, FileMode.Open);

            saveFile.objectSaved = formatter.Deserialize(stream) as SaveSystem.ObjectSaved;

            stream.Close();

            Debug.Log("Loaded");

        }
        else
        {
            Debug.Log("No Save File");

        }
    }
}
