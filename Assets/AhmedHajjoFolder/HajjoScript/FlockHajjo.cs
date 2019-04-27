using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockHajjo : MonoBehaviour
{
    private Rigidbody rb;
    public List<FlockHajjo> neighbours;
   [Range (0,10)]public float wanderPower = 1;
   [Range (0,10)]public float alignmentPower = 1;
   [Range (0,10)]public float cohesionPower = 1;
   [Range (0,10)]public float seperationPower = 1;
   [Range (0,10)]public float maximum = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        neighbours = new List<FlockHajjo>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = this.rb.velocity;
    }

    private void FixedUpdate()
    {
        this.rb.velocity += wanderPower * Wander(rb.position, rb.velocity);
        this.rb.velocity += alignmentPower * Alignment(neighbours);
        this.rb.velocity += cohesionPower * Cohesion(neighbours, rb.position);
        this.rb.velocity += seperationPower * Seperation(neighbours, rb.position);
        this.rb.velocity = Truncate(rb.velocity, maximum);
    }

    Vector3 Truncate(Vector3 vector, float maximum)
    {
        return (vector.magnitude > maximum) ? vector.normalized * maximum : vector;
    }

    Vector3 Wander(Vector3 position, Vector3 velocity)
    {
        Vector3 point = position + velocity.normalized;
        var theta = Random.Range(-Mathf.PI, Mathf.PI);
        var radius = 1;
        point.x += radius * Mathf.Cos(theta);
        point.y += radius * Mathf.Sign(theta);

        return point;
    }

    Vector3 Alignment(List<FlockHajjo> neighbours)
    {
        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }
        else
        {
            Vector3 velocity = Vector3.zero;
            for (int i = 0; i < neighbours.Count; i++)
            {
                velocity += neighbours[i].rb.velocity;
            }
            velocity /= neighbours.Count;
            return velocity;
        }

    }

    Vector3 Cohesion(List<FlockHajjo> neighbours, Vector3 position)
    {
        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }
        else
        {
            Vector3 centerOfMass = Vector3.zero;
            for (int i = 0; i < neighbours.Count; i++)
            {
                centerOfMass += neighbours[i].rb.position;
            }
            centerOfMass /= neighbours.Count;
            Vector3 velocity = centerOfMass - position;
            return velocity;
        }
    }

    Vector3 Seperation(List<FlockHajjo> neighbours, Vector3 position)
    {
        if (neighbours.Count == 0)
        {
            return Vector3.zero;
        }
        else
        {

            Vector3 centerOfMass = Vector3.zero;
            for (int i = 0; i < neighbours.Count; i++)
            {
                centerOfMass += neighbours[i].rb.position;
            }
            centerOfMass /= neighbours.Count;
            Vector3 velocity = centerOfMass - position;
            return velocity;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FlockHajjo>())
        {
            neighbours.Add(other.GetComponent<FlockHajjo>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<FlockHajjo>() && neighbours.Contains(other.GetComponent<FlockHajjo>()))
        {
            neighbours.Remove(other.GetComponent<FlockHajjo>());
        }
    }
}
