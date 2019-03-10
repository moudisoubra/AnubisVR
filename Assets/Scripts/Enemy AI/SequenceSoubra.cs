using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSoubra : NodeSoubra
{
    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        for (int i = 0; i < nodesList.Count; i++)
        {
            NodeSoubra.Result result = nodesList[i].Execute(BTS);

            if (result == Result.running)
            {
                return Result.running;
            }

            else if (result == Result.failure)
            {
                return Result.failure;
            }
        }

        return Result.success;
    }
}
