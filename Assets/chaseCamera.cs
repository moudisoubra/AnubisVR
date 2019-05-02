using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseCamera : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(camera.transform.position.x, this.transform.position.y, camera.transform.position.z);
    }
}
