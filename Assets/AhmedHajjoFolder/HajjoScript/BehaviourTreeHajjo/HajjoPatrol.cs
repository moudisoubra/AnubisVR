using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HajjoPatrol : NodesHajjo
{

    public int[] PathPoints;
    public int currentPoint = 0;
    public bool pathCollected;
    Vector3 pos;
    public bool isIdle;

    public override Results Execute(BTreeHajjo Bt)
    {

        Vector3 direction = Bt.selfObject.transform.position - Bt.transform.position;
        if (Vector3.Distance(Bt.selfObject.transform.position, Bt.transform.position) <= 5.5f)
        {

       
        if (!pathCollected)
        {
            currentPoint = 0;
            Vector3 reachPoint = DjisPathFindHajjo.instance.allNodes[Random.Range(0, DjisPathFindHajjo.instance.allNodes.Length)].trans.position;
            PathPoints = DjisPathFindHajjo.instance.DjiPath(Bt.selfObject.transform.position, reachPoint);

            pathCollected = true;
        }

        pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;
        pos.y = 0;


        //If im Close enough to way point go to next one 
        if (Vector3.Distance(Bt.selfObject.transform.position, pos) < 0.5f)
        {
            Bt.StartCoroutine(IdleWhenWayPointReached(Bt));
            direction.y = 0;
            currentPoint++;
        }
        else if (!isIdle)
        {
            Bt.animator.SetBool("isWalk", true);
            Move(Bt);
            direction.y = 0;
        }

        if (currentPoint == PathPoints.Length)
        {

            pathCollected = false;
            currentPoint = -1;
        }

        }

        Debug.Log("PATROL SUCCESS");
        return Results.success;
       
        
    }


    private IEnumerator IdleWhenWayPointReached(BTreeHajjo Bt) //IEnumerator on For Idle Make the Enemy Idle on reach eachWaypoint with random Range Time.
    {
        // Random Amount of time


        float randomTime = Random.Range(1, 3); //Random Range
        Debug.Log(" STOP MOVING");



        isIdle = true;
        Bt.animator.SetBool("isWalk", false); //Idle Animation
        yield return new WaitForSeconds(randomTime);  //Stops for Seconds
        isIdle = false; // Resume Move
        Debug.Log(" Resume MOVING");
    }


    void Move(BTreeHajjo Bt)  // Enemy Movement
    {
        //VectorMoveTowards  (Vector Current , Vector Target , Float MaxDelta
        Bt.selfObject.transform.position = Vector3.MoveTowards(Bt.selfObject.transform.position, pos, Time.deltaTime * 0.8f);
        pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position; //Vector Positon Current Waypoint
        pos.y = Bt.transform.position.y; //ReachPosition Uses Y position Only.
        Bt.transform.rotation = Quaternion.Slerp(Bt.transform.rotation, Quaternion.LookRotation(pos - Bt.transform.position), 0.2f);  //Enemy Rotation To look at WayPoint.
    }

}
