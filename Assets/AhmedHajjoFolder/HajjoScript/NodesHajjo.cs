using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesHajjo : MonoBehaviour
{
    public List<NodesHajjo> nodeList = new List<NodesHajjo>();

    public enum Results { success, running, failure };


    public virtual Results execute(BTreeHajjo Bt)
    {
        return Results.running;

    }
}
