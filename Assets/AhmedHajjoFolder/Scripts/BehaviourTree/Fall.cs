using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : Node {

    public override Result Execute(BehaviourTree BT)
    {
        if (!BT.fly)
        {
            BT.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log("Fall running");
            if (BT.time > 5)
            {
                Debug.Log("Fall Success");
                BT.fly = true;
                BT.time = 0;
                return Result.success;
            }
            return Result.running;

        }
        else
        {
            Debug.Log("Fall Fail");
            return Result.failure;
        }
    }
}
