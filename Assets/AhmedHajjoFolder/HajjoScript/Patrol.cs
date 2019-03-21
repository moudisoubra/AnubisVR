using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField]
    protected float debugDrawRadius = 1.0F;



    [SerializeField]
    bool patrolWaiting;

    [SerializeField]
    float totalWaitTime = 5f;


    [SerializeField]
    float switchProbability = 0.2f;

    [SerializeField]
    List<Patrol> patrols;

    NavMeshAgent navMesh;

    [SerializeField]
    Transform destination;

    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = this.GetComponent<NavMeshAgent>();

        if(navMesh == null)
        {
            Debug.Log("the nav is not attached" + gameObject.name);

        }

        else
        {
            setDistination();
        }

    }



    void setDistination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.transform.position;
            navMesh.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, debugDrawRadius);
            
    }
}
