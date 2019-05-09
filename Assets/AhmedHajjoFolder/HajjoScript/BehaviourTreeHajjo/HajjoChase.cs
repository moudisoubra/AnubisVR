using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HajjoChase : NodesHajjo
{
    public int[] PathPoints;

    public int currentPoint = 0;
    public int lastNodeID = -1;

    public LayerMask Wall;

    public bool pathCollected;

    public override Results Execute(BTreeHajjo Bt)
    {
        Vector3 direction = Bt.selfObject.transform.position - Bt.lastPoint.transform.position;
        float angle = Vector3.Angle(direction, Bt.transform.forward);
        direction.y = 0f;

        if (Vector3.Distance(Bt.selfObject.transform.position, Bt.lastPoint.position) <= 5f)
        {

            if (Vector3.Distance(Bt.selfObject.transform.position, Bt.lastPoint.position) <= 1.7f)
            {
                Debug.Log(Vector3.Distance(Bt.selfObject.transform.position, Bt.lastPoint.position));
                direction.y = 0f;
                Debug.Log("Chase success");
                return Results.success;
            }
            else
            {
                Bt.animator.SetBool("isChasing", true);
                Bt.animator.SetBool("isWalk", false);
                //Play Chase Animation
                direction.y = 0f;
                Bt.selfObject.transform.rotation = Quaternion.Slerp(Bt.transform.rotation, Quaternion.LookRotation(direction), 0.2f);
                Seeking(Bt);
                Debug.Log("Chase running");
                Debug.Log(Vector3.Distance(Bt.selfObject.transform.position, Bt.lastPoint.position));
                return Results.running;

            } 
         

               


            

     
        }

        Bt.animator.SetBool("isChasing", false);
        return Results.failure;



    }



    public void Seeking(BTreeHajjo Bt)
    {

        Vector3 DesiredVel = Bt.lastPoint.transform.position - Bt.selfObject.transform.position;
        DesiredVel = Vector3.Normalize(DesiredVel);
        DesiredVel *= Bt.maxVel;
        Vector3 seekForce = DesiredVel - Bt.rb.velocity;
        var p1 = Bt.lastPoint.transform.position;
        var p2 = Bt.transform.position;
        p2.y = p1.y;
        Bt.transform.rotation = Quaternion.Slerp(Bt.transform.rotation, Quaternion.LookRotation(p1 - p2), 0.4f);

        Bt.Move(seekForce);
    }


}
