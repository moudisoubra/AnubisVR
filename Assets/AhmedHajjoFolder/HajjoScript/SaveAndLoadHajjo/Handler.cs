using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        PlayerData playerData = new PlayerData();
        playerData.pos = new Vector3(5,0);
        playerData.health = 90;

        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);

        File.WriteAllText(Application.dataPath + "SAVEFile.json", json);



        //string json = File.ReadAllText(Application.dataPath + "SAVEFile.json");
       // PlayerData Loadplayer  =JsonUtility.FromJson<PlayerData>(json);
        //  Debug.Log("position:" + Loadplayer.pos);
        //Debug.Log("position:" + Loadplayer.health);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private class PlayerData
    {
        public Vector3 pos;
        public int health;
    }
}
