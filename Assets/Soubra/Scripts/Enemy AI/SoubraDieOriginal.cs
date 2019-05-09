using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoubraDieOriginal : NodeSoubra
{
    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        if (BTS.hitPoints >= BTS.maxHitPoints)
        {
            BTS.GetComponent<TestBodyParts>().Dead = true;
            Debug.Log("Dead Success");
            return Result.success;
        }
        Debug.Log("Dead Fail");
        return Result.failure;
    }
}