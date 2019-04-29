using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingHajjoTest : MonoBehaviour
{

    Rigidbody rb;
    public Transform seek;
    public Transform target;
    public float maxVel = 10f;

    Vector3 Alignment, Cohesion , Seperation;


    public List<FlockingHajjoTest> neighbours;
    int NeighborCount;
    Vector3 dis;
    FlockingHajjoTest myagent;
    //Vector3 myagent;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        neighbours = new List<FlockingHajjoTest>();
        NeighborCount = 0;
        Alignment = Cohesion = Seperation = Vector3.zero;
        rb.velocity = new Vector3(Random.Range(10, 100), 0, Random.Range(5, 50));
    }



    // Update is called once per frame
    void Update()
    {
        ComputeAlignment(neighbours);
    }



    public void seeker()
    {
       
        Vector3 DesiredVel = target.position - seek.position;
        DesiredVel = Vector3.Normalize(DesiredVel);
        DesiredVel *= 15f;
        Vector3 seekForce = DesiredVel - rb.velocity;

        Move(seekForce);

    }

    public void ComputeAlignment(List<FlockingHajjoTest> neighbours)
    {

    
        foreach (var Neighbour in neighbours)
        {
            float dis = Vector3.Distance(myagent.transform.position, Neighbour.transform.position);
            if (Neighbour != myagent)
            {
                if (dis < 20)
                {
                    Alignment += myagent.rb.velocity;
                    NeighborCount++;
                }
            }
        }

        if (NeighborCount == 0)
        {
            rb.velocity = Alignment;

        }
        else
        {
            rb.velocity /= NeighborCount;
            rb.velocity = rb.velocity.normalized;

        }
    }



    public void ComputeCohesion(List<FlockingHajjoTest> neighbours)
    {
       
    }

    public void ComputeSeperation(List<FlockingHajjoTest> neighbours)
    {
        
    }


    public void Move(Vector3 dir)
    {
        rb.AddForce(dir);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVel);
    }

    
}

