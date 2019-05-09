using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesHajjo : MonoBehaviour
{
    public List<NodesHajjo> ListOfNodes = new List<NodesHajjo>();

    public enum Results  { running, success, failure };


    public virtual Results Execute(BTreeHajjo Bt)
    {
        return Results.running;
    }
    
}
