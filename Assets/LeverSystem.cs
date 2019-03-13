using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverSystem : MonoBehaviour
{

    public GameObject wall;
    public GameObject distanceCheck;
    public GameObject distanceLimit;
    public Vector3 wallStartPosition;
    public float wallTransform;
    public bool movingWall;

    
    void Start()
    {
        wallStartPosition = wall.transform.position;
        wallTransform = 0;
    }

    // Update is called once per frame
    void Update()
    {

        wallTransform = Vector3.Distance(distanceCheck.transform.position, distanceLimit.transform.position);

        if (movingWall)
        {
            MoveWall();
        }
    }

    public void MoveWall()
    {
        wall.transform.position = new Vector3(wallStartPosition.x, wallStartPosition.y + (wallTransform * 4), wallStartPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand")
        {
            movingWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Hand")
        {
            movingWall = false;
        }
    }
}
