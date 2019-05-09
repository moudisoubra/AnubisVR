
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAJJODeath : NodesHajjo
{
    

    public override Results Execute(BTreeHajjo Bt)
    {
       
        if(Bt.Health <= 0)
        {
            Bt.GetComponent<TestBodyParts>().Dead = true;
            Debug.Log("Death Success");
            return Results.success;
        }
        else {

        
        Debug.Log("Dead Fail");
        return Results.failure;
        }

    }


}
