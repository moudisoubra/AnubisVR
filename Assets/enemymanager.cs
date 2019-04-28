using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymanager : MonoBehaviour
{

    public GameObject[] Enemies;
    public GameObject[] Enemies1;
    public GameObject[] Enemies2;
    public GameObject Wall;
    public GameObject Wall1;
    public GameObject Wall2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collided)
    {
        if (collided.gameObject == Wall)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i].gameObject.SetActive(true);

            }

        }
        if (collided.gameObject == Wall1)
        {
            for (int i = 0; i < Enemies1.Length; i++)
            {
                Enemies1[i].gameObject.SetActive(true);

            }
        }
        if (collided.gameObject==Wall2)
        {
            for (int i = 0; i < Enemies2.Length; i++)
            {
                Enemies2[i].gameObject.SetActive(true);

            }
        }
    }
}
