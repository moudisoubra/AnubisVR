using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorSoubra : NodeSoubra
{
    public override Result Execute(BehaviourTreeSoubra BTS)
    {
        for (int i = 0; i < nodesList.Count; i++)
        {
            NodeSoubra.Result result = nodesList[i].Execute(BTS);

            if (result == Result.success)
            {
                Debug.Log("Selector Success");
                return Result.success;
            }


            else if (result == Result.running)
            {
                Debug.Log("Selector Running");
                return Result.running;
            }
        }
        Debug.Log("Selector Fail");
        return Result.failure;

    }
}
