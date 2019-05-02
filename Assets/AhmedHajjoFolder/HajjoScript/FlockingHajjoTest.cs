using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingHajjoTest : MonoBehaviour
{

    Rigidbody rb;
    public Transform seek;
    public Transform target;
    public float maxVel = 10f;

    public List<FlockingHajjoTest> neighbours;
    float NeighborCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        neighbours = new List<FlockingHajjoTest>();
        NeighborCount = 0;
    }



    // Update is called once per frame
    void Update()
    {
        seeker();
    }


    public void seeker()
    {
        Vector3 DesiredVel = target.position - seek.position;
        DesiredVel = Vector3.Normalize(DesiredVel);
        DesiredVel *= 15f;
        Vector3 seekForce = DesiredVel - rb.velocity;

        Move(seekForce);

    }

    //public void ComputeAlignment(List<FlockingHajjoTest> neighbours)
    //{
    //   foreach ()
    //}



    public void Move(Vector3 dir)
    {
        rb.AddForce(dir);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVel);
    }

    
}

