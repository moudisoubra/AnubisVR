using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraAttack : NodeSoubra
{

  
    RaycastHit hit;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        if (Vector3.Distance(BTS.selfObject.transform.position, BTS.playerHead.transform.position) < BTS.MaxDis)
        {
            BTS.transform.LookAt(BTS.playerHead.transform);
            BTS.selfObject.transform.position = Vector3.MoveTowards(BTS.selfObject.transform.position, BTS.playerHead.transform.position, Time.deltaTime * BTS.speed);
            if (Vector3.Distance(BTS.selfObject.transform.position, BTS.playerHead.transform.position) < 2)
            {
                BTS.animator.SetTrigger("Attack");
                Debug.Log("Attack Success");
                return Result.success;
            }
            Debug.Log("Attack Running");
            return Result.running;
        }
        else
        {
            Debug.Log("Attack Failure");
            return Result.failure;
        }

        //if (BTS.fail)
        //{
        //    Debug.Log("Attack fail");
        //    return Result.failure;
        //}
        //Debug.Log("Attack success");
        //return Result.success;

    }
}
