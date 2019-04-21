using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCircle : MonoBehaviour
{
    public bool canRotate;
    public bool properlyRotated;
    public bool grab;
    public GameObject rotatingHand;
    public GameObject center;
    public Vector3 handPreLocation;
    public Vector3 crossProduct;
    public Transform circleStartRotation;
    public float resultCross;
    public float directionRL;
    public float distance;

    void Start()
    {
        circleStartRotation = transform;
    }


    void Update()
    {

        if (rotatingHand)
        {
        grab = rotatingHand.GetComponentInParent<AnubisController>().rightGrab;
            if ((rotatingHand.GetComponentInParent<AnubisController>().rightGrab
                || rotatingHand.GetComponentInParent<AnubisController>().leftGrab))
            {
                center = Instantiate(new GameObject(), rotatingHand.transform.position, Quaternion.identity);
                Vector3 targetPosition = rotatingHand.transform.position - center.transform.position;
                directionRL = GetDirectionLeftRight(center.transform.forward, targetPosition);
                Debug.Log("Here" + directionRL);

                distance = Vector3.Distance(handPreLocation, rotatingHand.transform.position);
                this.gameObject.transform.rotation = Quaternion.Euler(circleStartRotation.eulerAngles.x, circleStartRotation.eulerAngles.y, circleStartRotation.eulerAngles.z + (directionRL * distance));
                //this.gameObject.transform.Rotate(0, 0, directionRL * distance);

                if (rotatingHand.name == "Right" && !rotatingHand.GetComponentInParent<AnubisController>().rightGrab)
                {
                    rotatingHand = null;
                }
                if (rotatingHand.name == "Left" && !rotatingHand.GetComponentInParent<AnubisController>().leftGrab)
                {
                    rotatingHand = null;
                }
            }


        }


        if (this.transform.eulerAngles.z <= 11.7f && this.transform.eulerAngles.z >= 7.5f)
        {
            properlyRotated = true;
        }
        else
        {
            properlyRotated = false;
        }

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            rotatingHand = other.gameObject;
            handPreLocation = rotatingHand.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            Debug.Log("Exit");
        }
    }
}
