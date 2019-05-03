using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HajjoChase : NodeSoubra
{
    public int[] PathPoints;

    public int currentPoint = 0;
    public int lastNodeID = -1;

    public LayerMask Wall;

    public bool pathCollected = false;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
            Vector3 direction = BTS.selfObject.transform.position - BTS.transform.position;
            float angle = Vector3.Angle(direction, BTS.transform.forward);

        if (Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position) < 60 )
        {
/*
            //Chase 
            if (!pathCollected)
            {
                currentPoint = 0;

                PathPoints = DjisPathFindHajjo.instance.DjiPath(BTS.selfObject.transform.position, BTS.lastPoint.position);
                Debug.Log("New Path collected...");
                pathCollected = true;
            }
    */

            //Attack
            if (Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position) < 30)
            {
                direction.y = 0f;

                BTS.transform.rotation = Quaternion.Slerp(BTS.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
                Seeking(BTS);

                Debug.Log("Chase success");
                return Result.success;
            }
            if (Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position) < 50)
            {

                Debug.Log("Chase running");
            return Result.running;
            

            }
        }
        //Debug.Log(Vector3.Distance(BTS.selfObject.transform.position, BTS.lastPoint.position));
        return Result.failure;

        //if (/*no player*/ )
        //{


        //Debug.Log("Chase Failed");
        //return Result.success;

        //}






    }



    public void Seeking(BehaviourTreeSoubra BTS)
    {

        Vector3 DesiredVel = BTS.selfObject.transform.position - BTS.transform.position;
        DesiredVel = Vector3.Normalize(DesiredVel);
        DesiredVel *= 0.5f;
        Vector3 seekForce = DesiredVel - BTS.rb.velocity;

        //BTS.Move(seekForce);

    }


}
