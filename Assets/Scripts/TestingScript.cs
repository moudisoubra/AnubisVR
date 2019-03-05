using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    public float velocity;
    public Vector3 previous;

    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float forceX;
        forceX = Input.GetAxis("Horizontal") * speed;
        float forceZ;
        forceZ = Input.GetAxis("Vertical") * speed;

        GetComponent<Rigidbody>().AddForce(forceX, 0, forceZ);
    }
}
