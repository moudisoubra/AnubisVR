using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialManagment : MonoBehaviour
{
    private SpaceManager spaceManager;
    public Transform playerObject;
    public GameObject[] arrayOfObjects;
    // Start is called before the first frame update
    void Start()
    {
        spaceManager = GetComponent<SpaceManager>();
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Grid.instance.grid[spaceManager.currentNode.x, spaceManager.currentNode.y].ContainObjects.Contains(playerObject))
        {
            for (int i = 0; i < arrayOfObjects.Length; i++)
            {
                arrayOfObjects[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < arrayOfObjects.Length; i++)
            {
                arrayOfObjects[i].SetActive(false);
            }
        }
    }
}
