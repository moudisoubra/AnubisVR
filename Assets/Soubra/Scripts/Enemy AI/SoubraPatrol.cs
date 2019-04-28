using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraPatrol : NodeSoubra
{

    public int[] PathPoints;

    public int currentPoint = 0;
    public int lastNodeID = -1;

    public LayerMask Wall;

    public bool pathCollected = false;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        Vector3 pos;

        if (!pathCollected)
        {
            currentPoint = 0;

                Vector3 reachPoint = DjisPathFindHajjo.instance.allNodes[Random.Range(0, DjisPathFindHajjo.instance.allNodes.Length)].trans.position;
                PathPoints = DjisPathFindHajjo.instance.DjiPath(BTS.selfObject.transform.position, reachPoint);

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

        Debug.Log("PATROL SUCCESS");
        return Result.success;
    }
}
