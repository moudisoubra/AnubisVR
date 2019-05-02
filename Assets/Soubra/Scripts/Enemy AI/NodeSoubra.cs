using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHajjo : MonoBehaviour
{
    public List<NodeHajjo> nodesList = new List<NodeHajjo>();

    public enum Result { running, success, failure };

    public virtual Result Execute(BehaviourTreeSoubra BTS)
    {
        return Result.running;
    }
}
