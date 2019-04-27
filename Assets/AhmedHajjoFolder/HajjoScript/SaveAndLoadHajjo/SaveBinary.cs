using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveBinary
{
   

    public static void SavePlayer(Handler.PlayerData sendData)
    {

        //Binary Formatter allows to write data to a file
        BinaryFormatter formatter = new BinaryFormatter();

        //Create A File or Open File to save To
        string path = Application.persistentDataPath + "/Player.fun";
        FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

        //Serialization method to write to the File
        formatter.Serialize(stream, sendData);
        stream.Close();
        Debug.Log("position:" + sendData.mypos.getPos());
        Debug.Log("Saving");
    }

    

    public static void Load(Handler src)
    {
        string file = Application.persistentDataPath + "/Player.fun";

        if (File.Exists(file))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(file, FileMode.Open);
            
            src.data = formatter.Deserialize(stream) as Handler.PlayerData;
            Debug.Log(src.data.mypos.getPos());

            stream.Close();

            Debug.Log("loading");
            
        }
        else
        {
            Debug.Log("Save file not found in " + file);
          
        }
    }

}
