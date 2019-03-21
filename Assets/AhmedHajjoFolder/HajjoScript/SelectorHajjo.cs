using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorHajjo : NodesHajjo
{
    public override Results execute(BTreeHajjo Bt)
    {
        for (int i = 0; i < nodeList.Count; i++)
        {
            NodesHajjo.Results results = nodeList[i].execute(Bt);

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
