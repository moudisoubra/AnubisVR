using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    public float circleRadius;
    public float randomTargetPercent;
    public float maxRadius;
    public float maxSpeed;
    public float maxForce;
    public float circleDistance;
    Vector3 desiredVelocity;
    Vector3 circleCenter;
    Vector3 randomCenter;
    Vector3 toCircleCenter;
    Vector3 displacement;
    Vector3 velocity;
    Vector3 wanderTarget;
    Vector3 randomTarget;

    Rigidbody cubeRigid;

    public float time;

    public void Start()
    {
        time = 4;
        cubeRigid = gameObject.GetComponent<Rigidbody>();
    }

    public void Update()
    {
        time += Time.deltaTime;

        desiredVelocity = wanderDirection();
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        desiredVelocity = desiredVelocity - velocity;
        desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, maxForce);
        desiredVelocity /= cubeRigid.mass;

        velocity = Vector3.ClampMagnitude(velocity + desiredVelocity, maxSpeed);
        velocity.y = 0;

        transform.position += velocity * Time.deltaTime;
        transform.forward = velocity.normalized;
    }

    public Vector3 wanderDirection()
    {
        if (transform.position.magnitude > maxRadius)
        {
            randomTarget = new Vector3(Random.Range(0.0f, 50.0f), Random.Range(0.0f, 50.0f), Random.Range(0.0f, 50.0f));
            toCircleCenter = (randomTarget - transform.position).normalized;
            wanderTarget = velocity.normalized + toCircleCenter;time = 0;
        }
        else if (time > 3)
        {
            circleCenter = velocity.normalized;
            circleCenter = circleCenter * circleDistance;
            randomCenter = Random.insideUnitCircle;

            displacement = new Vector3(randomCenter.x, randomCenter.y) * circleRadius;
            displacement = Quaternion.LookRotation(velocity) * displacement;
            wanderTarget = circleCenter + displacement;
            time = 0;
        }
        return wanderTarget;
    }
}
