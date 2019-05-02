using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public GameObject floorMovable;
    public float changeRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (floorMovable.transform.position.z < 602)
        {
            floorMovable.transform.position = new Vector3(floorMovable.transform.position.x, floorMovable.transform.position.y, floorMovable.transform.position.z + changeRate);
        }
    }
}
