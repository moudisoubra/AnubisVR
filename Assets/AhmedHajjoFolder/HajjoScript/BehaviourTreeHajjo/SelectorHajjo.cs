using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorHajjo : NodesHajjo
{
    public override Results Execute(BTreeHajjo Bt)
    {
        for (int i = 0; i < ListOfNodes.Count; i++)
        {
            Results results = ListOfNodes[i].Execute(Bt);

            if (results == Results.success)
            {
                Debug.Log("Selector Node Successed");
                return Results.success;
            }

            else if (results == Results.running)
            {
                Debug.Log("Selector Running");
                return Results.running;

            }
        }
        Debug.Log("Selector Fail");
        return Results.failure;






    }

}
