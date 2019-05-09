using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSoubra : MonoBehaviour
{
    public List<NodeSoubra> nodesList = new List<NodeSoubra>();

    public enum Result { running, success, failure };

    public virtual Result Execute(BehaviourTreeSoubra BTS)
    {
        return Result.running;
    }
}
