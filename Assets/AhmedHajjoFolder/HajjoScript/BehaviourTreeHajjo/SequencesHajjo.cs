using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencesHajjo : NodesHajjo
{
    public override Results Execute(BTreeHajjo Bt)
    {
        for(int i = 0; i < ListOfNodes.Count; i++)
        {
            Results results = ListOfNodes[i].Execute(Bt);

            if (results == Results.failure)
            {
                Debug.Log("Sequences Failure");
                return Results.failure;
            }

           else if (results == Results.running)
            {

                Debug.Log("Sequences Running");
                return Results.running;
            }

         
        }
        return Results.success;

        
    }
}
