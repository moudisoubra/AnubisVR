using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHajjo : NodesHajjo
{

    public override Results Execute(BTreeHajjo Bt)
    {
        Vector3 direction = Bt.selfObject.transform.position - Bt.lastPoint.transform.position;
        direction.y = 0f;
        if (Vector3.Distance(Bt.selfObject.transform.position, Bt.lastPoint.transform.position) < 1.7f)
        {

            Bt.transform.rotation = Quaternion.Slerp(Bt.transform.rotation, Quaternion.LookRotation(-direction), 0.2f);
            //     Bt.transform.eulerAngles = new Vector3(0, Bt.selfObject.transform.eulerAngles.y, 0);
            Bt.animator.SetTrigger("Attack");
            Bt.animator.SetBool("isWalk", false);

            Debug.Log("ATTsuccess");
            return Results.success;
        }
        Debug.Log("ATTfail");
        return Results.failure;


    }

}
