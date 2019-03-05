using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Node> nodesList = new List<Node>();

    public enum Result { running, success, failure };

    public virtual Result Execute(BehaviourTree BT)
    {
        return Result.running;
    }
}
