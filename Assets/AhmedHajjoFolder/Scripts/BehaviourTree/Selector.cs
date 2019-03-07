using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node {

    public override Result Execute(BehaviourTree BT)
    {
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            Node.Result result = childrenNodes[i].Execute(BT);

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
