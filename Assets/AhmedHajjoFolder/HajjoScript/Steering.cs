using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
public class Steering : MonoBehaviour
{
    public Vector2 desiredVelocity;
    public Rigidbody2D body;
    public Transform target;
    [Range(0, 1)]
    public float seekForce = 1;
    public float fleeForce = 1;
    public float T;
    public float maxSpeed;








    private List<Steering> others;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        this.transform.up = this.body.velocity;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //this.body.velocity += seekForce * Seek(body.position, target.position);
        //var distance = (target.position - this.transform.position).magnitude;
        //var time = Arrive(distance, slowingDistance, T);
        //this.body.velocity = trunCate(body.velocity, maxSpeed);








    }



    //Vector2 Chase(Vector2 self, Vector2 target, Vector2 velocity, float time)
    //{
    //    var FuturePosition = target + velocity * time;

    //}

    Vector2 trunCate(Vector2 vector, float maximum)
    {
        if (vector.magnitude > maximum)
        {
            vector = vector.normalized * maximum;

        }

        return vector;
    }

    Vector2 Steer(Vector2 currentVelocity, Vector2 desireVelocity)
    {
        return (desireVelocity.normalized - currentVelocity.normalized).normalized;
    }

    Vector2 Seek(Vector2 Self, Vector2 target)
    {
        return (target - Self).normalized;
    }

    float Arrive(float distance, float slowingDistance, float maxSpeed)
    {
        if (distance < slowingDistance)
        {
            var a = maxSpeed / slowingDistance;
            return distance * a;
        }
        return maxSpeed;
    }


    Vector2 Wander(Vector2 Position, Vector2 velocity)
    {
        Vector2 point = Position + velocity.normalized * 2;
        var theta = Random.Range(-Mathf.PI, Mathf.PI);
        var r = 1;
        point.x += r * Mathf.Cos(theta);
        point.y += r * Mathf.Sin(theta);
        return point;
    }

    Vector2 Alignment(List<Steering> others)
    {
        Vector2 velocity = Vector2.zero;
        for (int i = 0; i < others.Count; i++)
        {
            velocity += others[i].body.velocity;
        }
        velocity /= others.Count;


        return velocity;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var boid = collision.GetComponent<Steering>();
    //    if (boid &&)
    //}
}
