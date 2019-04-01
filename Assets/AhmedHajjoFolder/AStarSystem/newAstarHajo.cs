using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newAstarHajo : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("msg send...");
            AStarPathTestSystem.instance.ReturnRout(startPoint, lastPoint);
        }
    }

}
