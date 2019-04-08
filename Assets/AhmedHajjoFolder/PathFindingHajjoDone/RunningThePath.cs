using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningThePath : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    bool butoonUp = false;
    [SerializeField] private int[] PathPoints;
    [SerializeField] private bool isChasing = true;
    private int currentPoint = 0;
    private int lastNodeID = -1;

    public LayerMask Wall;

    private bool pathCollected = false;

    int goToNode;

    float speed = 10f;

    bool veryNear = false;

    float MaxDis = 5f;

    // Update is called once per frame
    void Update()
    {

        Vector3 dir = lastPoint.position - transform.position;
        if (!Physics.Raycast(transform.position, dir, MaxDis,Wall) && dir.magnitude <MaxDis) 
        {
            //seek the player
            transform.position = Vector3.MoveTowards(transform.position, lastPoint.position, Time.deltaTime * speed);
            Debug.Log("MOVE TOWERDS...");
            pathCollected = false;
        }
        else
        {
            Debug.Log("MOVE ALONG...");
            if (!pathCollected)
            {
                currentPoint = 0;
                //PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
                Vector3 reachPoint = DjisPathFindHajjo.instance.allNodes[Random.Range(0, DjisPathFindHajjo.instance.allNodes.Length)].trans.position;
                PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, reachPoint);
                pathCollected = true;
            }

            Vector3 pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;
          



            if (Vector3.Distance(transform.position, pos) < 0.1f)
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
                currentPoint = 0;
            }

        }











        //if (butoonUp && Input.GetKeyDown(KeyCode.I))
        //{
        //    Debug.Log("Dijkstra msg send...");

        //    butoonUp = false;
        //}

        //else if (Input.GetKeyUp(KeyCode.I))
        //{
        //    butoonUp = true;


        //}






    }





    private void OnDrawGizmos()
    {



        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 5f);
    }

}


