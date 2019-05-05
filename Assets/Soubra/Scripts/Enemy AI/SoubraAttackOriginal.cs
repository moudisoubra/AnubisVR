using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraAttackOriginal : NodeSoubra
{
    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        if (BTS.fail)
        {
            Debug.Log("Attack fail");
            return Result.failure;
        }
        Debug.Log("Attack success");
        return Result.success;
    }
}
