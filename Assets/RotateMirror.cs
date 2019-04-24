using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMirror : MonoBehaviour
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


    public Vector3 centerToPrevious;
    public Vector3 centerToCurrent;
    public float angleChange;
    public float lastAngle;
    public float differenceAngle;

    void Start()
    {
        circleStartRotation = transform;
    }


    void Update()
    {

        if (rotatingHand)
        {
            //grab = rotatingHand.GetComponentInParent<AnubisController>().rightGrab;
            //if ((rotatingHand.GetComponentInParent<AnubisController>().rightGrab
            //    || rotatingHand.GetComponentInParent<AnubisController>().leftGrab))
            //{
            centerToCurrent = center.transform.position - rotatingHand.transform.position;
            //angleChange = Vector3.Angle(centerToPrevious, centerToCurrent);
            angleChange = Vector3.SignedAngle(centerToPrevious, centerToCurrent, Vector3.up);
            centerToPrevious = centerToCurrent;



            this.gameObject.transform.localEulerAngles += new Vector3(0, angleChange, 0);

            if (rotatingHand.name == "Right" && !rotatingHand.GetComponentInParent<AnubisController>().rightGrab)
            {
                rotatingHand = null;
            }
            if (rotatingHand.name == "Left" && !rotatingHand.GetComponentInParent<AnubisController>().leftGrab)
            {
                rotatingHand = null;
            }
            //}


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
            centerToPrevious = center.transform.position - handPreLocation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hand")
        {
            rotatingHand = collision.gameObject;
            handPreLocation = rotatingHand.transform.position;
            centerToPrevious = center.transform.position - handPreLocation;
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
