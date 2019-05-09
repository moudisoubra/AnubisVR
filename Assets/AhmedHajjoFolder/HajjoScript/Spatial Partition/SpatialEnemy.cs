using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialEnemy : MonoBehaviour
{
    private SpaceManager spaceManager;
    public float dis = 5;
    public Transform playerObject;
    
    // Start is called before the first frame update
    void Start()
    {
        spaceManager = GetComponent<SpaceManager>();
        playerObject = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GridCreator.instance.spacePar.grid[spaceManager.currentNode.x, spaceManager.currentNode.y].ContainObjects.Contains(playerObject))
        {
            Debug.Log("Player Found..");
            if (Vector3.Distance(transform.position, playerObject.position) < dis)
            {
                transform.Translate((playerObject.position - transform.position).normalized);
            }
        }
    }
    
}
