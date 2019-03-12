using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForceToItems : MonoBehaviour
{
    public bool isHeld;
    public GameObject objectPushed;
    public float pushFactor;

    // Start is called before the first frame update
    void Start()
    {
        objectPushed = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent && this.transform.parent.GetComponentInParent<AnubisController>())
        {
            if (this.transform.parent.GetComponentInParent<BoxCollider>().gameObject.name == "Right")
            {
                if (objectPushed && objectPushed.GetComponent<Rigidbody>())
                {

                    objectPushed.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetAngularVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().maxAngularVelocity = objectPushed.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
                    objectPushed = null;
                }
            }
            else
            {
                if (objectPushed && objectPushed.GetComponent<Rigidbody>())
                {

                    objectPushed.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetAngularVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().maxAngularVelocity = objectPushed.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
                    objectPushed = null;
                }
            }

        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponent<Items>())
        {
            objectPushed = collision.gameObject;
        }
    }

}
