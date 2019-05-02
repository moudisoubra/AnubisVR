using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingHajjo : MonoBehaviour
{

    Rigidbody rb;
    public Transform seek;
    public Transform target;
    public float ForcePower = 20f;
    public float velocityLimit = 10f;

    Vector3 Alignment, Cohesion, Seperation;


    //public List<FlockingHajjoTest> neighbours;
    public FlockingHajjo[] neighbours;
    int NeighborCount;
    Vector3 dis;
    FlockingHajjo myagent;
    public bool Seeker; 
    //Vector3 myagent;
    // Start is called before the first frame update
    void Start()
    {
        myagent = this;
        rb = GetComponent<Rigidbody>();
        neighbours = FindObjectsOfType<FlockingHajjo>();
        NeighborCount = 0;
        Alignment = Cohesion = Seperation = Vector3.zero;
        //rb.velocity = new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100));


    }



    // Update is called once per frame
    void FixedUpdate()
    {
        ComputeAlignment(neighbours);
        ComputeCohesion(neighbours);
        ComputeSeperation(neighbours);

        Debug.Log(myagent.rb.position);

        if (!Seeker)
        {
            Move(Alignment + Cohesion + Seperation);
        }
        else
        {
            seeker();
        }

    }






    public void seeker()
    {

        Vector3 DesiredVel = target.position - transform.position;
        DesiredVel = Vector3.Normalize(DesiredVel);
        DesiredVel *= 15f;
        Vector3 seekForce = DesiredVel - rb.velocity;

        Move(seekForce);

    }

    public void ComputeAlignment(FlockingHajjo[] neighbours)
    {
        Alignment = Vector3.zero;

        foreach (var Neighbour in neighbours)
        {
            float dis = Vector3.Distance(myagent.transform.position, Neighbour.transform.position);
            if (Neighbour != myagent)
            {
                if (dis < 10)
                {
                    Alignment += Neighbour.rb.velocity;
                }
            }
        }

        if (neighbours.Length == 0)
        {
            Alignment = Vector3.zero;


        }
        else
        {
            Alignment /= neighbours.Length;
            Alignment.Normalize();

        }
    }



    public void ComputeCohesion(FlockingHajjo[] neighbours)
    {
        Cohesion = Vector3.zero;


        foreach (var neighbour in neighbours)
        {
            float dis = Vector3.Distance(myagent.transform.position, neighbour.transform.position);
            if (neighbour != myagent)
            {
                if (dis > 10f)
                {
                    Cohesion += neighbour.transform.position - myagent.rb.position;

                }
            }

        }

        if (neighbours.Length == 0)
        {
            Cohesion = Vector3.zero;
        }

        else
        {
            Cohesion /= neighbours.Length;
            Cohesion.Normalize();
        }


    }

    public void ComputeSeperation(FlockingHajjo[] neighbours)
    {

        Seperation = Vector3.zero;

        foreach (var neighbour in neighbours)
        {

            float dis = Vector3.Distance(myagent.transform.position, neighbour.transform.position);
            if (neighbour != myagent)
            {
                if (dis < 2)
                {
                    Seperation += myagent.rb.position - neighbour.rb.position;

                }

            }
            if (neighbours.Length == 0)
            {
                Seperation = Vector3.zero;
            }

            else
            {
                Seperation /= neighbours.Length;
                Seperation.Normalize();
            }
        }


    }


    public void Move(Vector3 dir)
    {
        rb.AddForce(dir * ForcePower);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, velocityLimit);
    }


}

