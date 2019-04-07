using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPath : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    bool butoonUp = true;

    float speed = 10f;

    private bool pointsAquired = false;
    [SerializeField] private int[] travelPoints;
    private int currentPoint = 0;
    [SerializeField] private bool isChasing = true;
    bool veryNear = false;
    LayerMask Wall;

    // Update is called once per frame
    void Update()
    {/*
        if (butoonUp && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Dijkstra msg send...");

            butoonUp = false;
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            butoonUp = true;
            DjisPathFindHajjo.instance.DjiPath(startPoint.position, lastPoint.position);



        }
            startPoint.position = Vector3.MoveTowards(startPoint.position, lastPoint.position, Time.deltaTime * speed);
        */


        Vector3 dir = lastPoint.position - transform.position;
        if (!Physics.Raycast(transform.position, dir, dir.magnitude, Wall) && dir.magnitude < 5f)
        {
            veryNear = true;
            if (!isChasing) isChasing = true;

        }

        else
        {
            if (veryNear)
            {
                pointsAquired = false;
                veryNear = false;
            }
            if (isChasing) isChasing = false;
        }
        /*      

              else
              {
                  veryNear = false;

              }
      */
        if (!pointsAquired)
        {
            travelPoints = null;
            currentPoint = 0;
            pointsAquired = true;

            if (isChasing && !veryNear)
            {
                // chase system..
               // travelPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
            }

            if (!isChasing)
            {
                // patrolling system..
                Vector3 reachPoint = DjisPathFindHajjo.instance.allNodes[Random.Range(0, DjisPathFindHajjo.instance.allNodes.Length)].trans.position;
               travelPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, reachPoint);
            }
        }
        Vector3 pos = Vector3.zero;
        if (!veryNear)
        {
            pos = DjisPathFindHajjo.instance.allNodes[travelPoints[currentPoint]].trans.position;
        }

        else
        {
            pos = lastPoint.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, pos) <  0.5f)
        {
            if (!veryNear && currentPoint != travelPoints.Length - 1)
            {
                currentPoint++;
            }
            else
            {
                if (isChasing) veryNear = true;
                pointsAquired = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(DjisPathFindHajjo.instance.allNodes[travelPoints[currentPoint]].trans.position, 0.5f);


        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}


