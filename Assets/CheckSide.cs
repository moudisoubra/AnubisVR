using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSide : MonoBehaviour
{
    public Vector3 crossProduct;
    public float dotProduct;
    public Transform target;
    public float directionRL;
    public float directionIB;
    public float resultCross;
    public float resultDot;


    void Update()
    {
        Vector3 targetPosition = target.position - transform.position;

        directionRL = GetDirectionLeftRight(transform.forward, targetPosition);
        directionIB = GetDirectionForwardBack(transform.forward, targetPosition);
    }


    public float GetDirectionLeftRight(Vector3 forward, Vector3 targetDirection)
    {
        crossProduct = Vector3.Cross(forward, targetDirection).normalized;
        resultCross = crossProduct.y;

        if (resultCross > 0f)
        {
            Debug.Log("Left");
            return 1f;
        }
        else if (resultCross < 0f)
        {
            Debug.Log("Right");
            return -1f;
        }
        else
        {
            Debug.Log("Neither");
            return 0f;
        }
    }

    public float GetDirectionForwardBack(Vector3 forward, Vector3 targetDirection)
    {
        dotProduct = Vector3.Dot(targetDirection, target.transform.forward);
        resultDot = dotProduct;

        if (resultDot > 0f)
        {
            Debug.Log("Infront");
            return 1f;
        }
        else if (resultDot < 0f)
        {
            Debug.Log("Behind");
            return -1f;
        }
        else
        {
            Debug.Log("Neither");
            return 0f;
        }
    }
}
