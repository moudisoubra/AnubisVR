using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraChase : NodeSoubra
{
    public int[] PathPoints;

    public int currentPoint = 0;
    public int lastNodeID = -1;

    public LayerMask Wall;

    public bool pathCollected = false;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        if (Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position) <= BTS.distanceToChase && !BTS.chaseFail)
        {


            Vector3 pos;

            if (!pathCollected)
            {
                currentPoint = 0;

                PathPoints = DjisPathFindHajjo.instance.DjiPath(BTS.selfObject.transform.position, BTS.lastPoint.position);

                pathCollected = true;
            }

            pos = DjisPathFindHajjo.instance.allNodes[PathPoints[currentPoint]].trans.position;



            if (Vector3.Distance(BTS.selfObject.transform.position, pos) < 0.5f)
            {
                currentPoint++;
            }
            else
            {
                BTS.selfObject.transform.position = Vector3.MoveTowards(BTS.selfObject.transform.position, pos, Time.deltaTime * BTS.speed);
            }

            if (currentPoint == PathPoints.Length)
            {
                pathCollected = false;
                currentPoint = -1;
            }

            if (Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position) <= 15)
            {
                Debug.Log("Chase success");
                return Result.success;
            }

            Debug.Log("Chase running");
            return Result.running;
        }
        Debug.Log("Chase Failed");
        return Result.success;
    }
}
