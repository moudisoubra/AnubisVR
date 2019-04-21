using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningThePath : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    [SerializeField] private int[] PathPoints;

    private int currentPoint = 0;
    private int lastNodeID = -1;

    public LayerMask Wall;

    private bool pathCollected = false;

    float speed = 10f;
    float MaxDis = 5f;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos;
        Vector3 dir = lastPoint.position - transform.position;
        if (!Physics.Raycast(transform.position, dir, MaxDis,Wall) && dir.magnitude <MaxDis) 
        {
            //seek the player
            transform.position = Vector3.MoveTowards(transform.position, lastPoint.position, Time.deltaTime * speed);
            pathCollected = false;
        }
        else
        {
  
            if (!pathCollected)
            {
                currentPoint = 0;
                //PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
                Vector3 reachPoint = DjisPathFindHajjo.instance.allNodes[Random.Range(0, DjisPathFindHajjo.instance.allNodes.Length)].trans.position;
                PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, reachPoint);
                pathCollected = true;
            }

                 pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;



                if (Vector3.Distance(transform.position, pos) < 0.5f)
                {
                    currentPoint++;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);
                }

                if (currentPoint == PathPoints.Length)
                {
                    pathCollected = false;
                    currentPoint = -1;
                }
            
        }
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}


