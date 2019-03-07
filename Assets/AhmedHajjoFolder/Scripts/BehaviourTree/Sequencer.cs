using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : Node {

    public override Result Execute(BehaviourTree BT)
    {
        for (int i = 0; i < childrenNodes.Count; i++)
        {
            Node.Result result = childrenNodes[i].Execute(BT);

            if (result == Result.running)
            {
                return Result.running;
            }

            else if (result == Result.failure)
            {
                return Result.failure;
            }
        }

        return Result.success;
    }
}
