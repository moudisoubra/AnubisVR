using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool objectHeld;
    public GameObject currentController;
    public GameObject rightController;
    public GameObject leftController;
    public GameObject currentProperPosition;
    public GameObject rightProperPosition;
    public GameObject leftProperPosition;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            if (transform.parent.name == "RightModel")
            {
                currentController = rightController;
            }
            else if (transform.parent.name == "LeftModel")
            {
                currentController = leftController;
            }
        }


        if (currentController == leftController)
        {
            currentProperPosition = leftProperPosition;
        }

        if (currentController == rightController)
        {
            currentProperPosition = rightProperPosition;
        }

        if (objectHeld)
        {
            this.transform.position = currentProperPosition.transform.position;
            this.transform.rotation = currentProperPosition.transform.rotation;
        }
        //if (objectHeld)
        //{
        //    Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);

        //    foreach (Collider col in colliders)
        //    {
        //        if (col.gameObject.tag == "Hand" && !col.gameObject.GetComponent<Hands>().objectHeld && col.gameObject.GetComponent<Hands>().grabDown)
        //        {
        //            transform.parent = col.gameObject.transform;
        //        }

        //    }
        //}
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "NLeft")
        {
            currentController = leftController;
        }
        if (collision.transform.name == "NRight")
        {
            currentController = rightController;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.name == "NLeft")
        {
            currentController = null;
        }
        if (collision.transform.name == "NRight")
        {
            currentController = null;
        }
    }
}
