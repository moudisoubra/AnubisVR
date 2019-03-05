using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public override Result Execute(BehaviourTree BT)
    {
        for (int i = 0; i < nodesList.Count; i++)
        {
            Node.Result result = nodesList[i].Execute(BT);

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
