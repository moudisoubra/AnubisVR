using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    public PlayerData data;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {

        //PlayerData playerData = new PlayerData();
        //playerData.pos = new Vector3();
        //playerData.health = 90;

        //string json = JsonUtility.ToJson(playerData);
        //Debug.Log(json);

        //File.WriteAllText(Application.dataPath + "SAVEFile.json", json);



        //string json = File.ReadAllText(Application.dataPath + "SAVEFile.json");
        // PlayerData Loadplayer  =JsonUtility.FromJson<PlayerData>(json);
        //  Debug.Log("position:" + Loadplayer.pos);
        //Debug.Log("position:" + Loadplayer.health);

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void savePlayer()
    {

        PlayerData sendData = new PlayerData();
        sendData.health = 100;
        sendData.mypos.setPos(player.position);

        SaveBinary.SavePlayer(sendData);
       
    }

    public void loadPlayer()
    {
       
        SaveBinary.Load(this);
        player.position = data.mypos.getPos();
    }

    [System.Serializable]
    public struct SerializableVector3
    {
        public float x;
        public float y;
        public float z;

        public Vector3 getPos()
        {
            return new Vector3(x, y, z);
        }

        public void setPos(Vector3 pos)
        {
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }
    }


    [System.Serializable]
    public class PlayerData
    {
      
        public int health;
        public SerializableVector3 mypos;
       
    }
}
