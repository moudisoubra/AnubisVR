using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningThePath : MonoBehaviour
{

    [SerializeField] Transform startPoint;
    [SerializeField] Transform lastPoint;
    bool butoonUp = true;

    // Update is called once per frame
    void Update()
    {
        if (butoonUp && Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Dijkstra msg send...");

            butoonUp = false;
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            butoonUp = true;
        }
        DjisPathFindHajjo.instance.DjiPath(startPoint.position, lastPoint.position);
    }
}

