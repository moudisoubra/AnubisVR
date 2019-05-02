
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAJJODeath : NodeSoubra
{
    int health;

    public override Result Execute(BehaviourTreeSoubra BTS)
    {
       
        if(health == 0)
        {
            return Result.success;
        }

        return Result.failure;

    }


}
