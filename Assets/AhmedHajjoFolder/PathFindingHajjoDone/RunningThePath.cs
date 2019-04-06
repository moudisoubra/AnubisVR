using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningThePath : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    bool butoonUp = false;
    [SerializeField] private int[] PathPoints;
    private int currentPoint = 0;
    private int lastNodeID = -1;

    private bool pathCollected = false;

    int goToNode;

    float speed = 10f;
    
    bool veryNear = false;


    // Update is called once per frame
    void Update()
    {
        if (!pathCollected)
        {
            PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
            pathCollected = true;
        }

        Vector3 pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;


        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, pos) < 0.1f)
        {
            currentPoint++;
        }

        if (currentPoint == PathPoints.Length)
        {
            pathCollected = false;
            currentPoint = 0;
        }

/*
        else
        {
            currentPoint = 0;
        }
*/

        /*
         *        PathPoints = DjisPathFindHajjo.instance.DjiPath(transform.position, lastPoint.position);
               Vector3 pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;


                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);




                if (Vector3.Distance(transform.position, pos) < 0.05f)
                {
                    lastNodeID = currentPoint;
                    currentPoint++;
                }


                if(lastNodeID > 1)
                {
                    lastNodeID = 0;
                    currentPoint = 0;
                }




         * */










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


