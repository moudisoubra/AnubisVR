using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Node {

    public override Result Execute(BehaviourTree BT)
    {
        if (BT.fly)
        {
            Debug.Log("Fly running");

            BT.GetComponent<Rigidbody>().useGravity = false;
            BT.transform.position += new Vector3(0, BT.speed, 0) * Time.deltaTime;

            if (BT.time > 5)
            {
                Debug.Log("Fly Success");
                BT.fly = false;
                BT.time = 0;
                return Result.success;
            }

            return Result.running;

        }
        else
        {
            Debug.Log("Fly Fail");
            return Result.failure;
        }
    }
}
