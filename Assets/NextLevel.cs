using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public MoveFloorsSideways script;
    public Vector3 sendPlayer;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (script.sceneLoaded)
        {
            sendPlayer = GameObject.Find("Level2Position").transform.position;
            player.transform.position = sendPlayer;
            DontDestroyOnLoad(player);
        }
    }
}
