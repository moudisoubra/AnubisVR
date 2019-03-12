using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AddForceToItems : MonoBehaviour
{
    public float pushFactor;
    public GameObject objectPushed;

    public GameObject parentBone;
    public GameObject childBone;

    // Start is called before the first frame update
    void Start()
    {
        objectPushed = null;
    }

    // Update is called once per frame
    void Update()
    {

        if (this.transform.parent && this.transform.parent.GetComponentInParent<AnubisController>() && this.transform.gameObject.name != "Right" && this.transform.gameObject.name != "Left")
        {
            if (this.transform.parent.GetComponentInParent<BoxCollider>().gameObject.name == "Right")
            {
                if (objectPushed && objectPushed.GetComponent<Rigidbody>())
                {
                    if (objectPushed.transform.parent && objectPushed.transform.parent.GetComponentInParent<EnemyScript>())
                    {
                        parentBone = objectPushed.transform.parent.gameObject;
                        childBone = objectPushed.transform.transform.GetChild(0).gameObject;
                        Debug.Log(parentBone + " " + childBone);

                        ApplyEffectRight(parentBone);
                        ApplyEffectRight(childBone);
                    }
                    else
                    {
                        parentBone = null;
                        childBone = null;
                    }

                    ApplyEffectRight(objectPushed);
                    objectPushed = null;

                }
            }
            else
            {
                if (objectPushed && objectPushed.GetComponent<Rigidbody>())
                {
                    if (objectPushed.transform.parent && objectPushed.transform.parent.GetComponentInParent<EnemyScript>())
                    {
                        parentBone = objectPushed.transform.parent.gameObject;
                        childBone = objectPushed.transform.transform.GetChild(0).gameObject;
                        Debug.Log(parentBone + " " + childBone);

                        ApplyEffectLeft(parentBone);
                        ApplyEffectLeft(childBone);
                    }
                    else
                    {
                        parentBone = null;
                        childBone = null;
                    }
                    ApplyEffectLeft(objectPushed);
                    objectPushed = null;
                }
            }

        }
        else if (this.transform.gameObject.name == "Right")
        {
            if (objectPushed && objectPushed.GetComponent<Rigidbody>())
            {
                if (objectPushed.transform.parent && objectPushed.transform.parent.GetComponentInParent<EnemyScript>())
                {
                    parentBone = objectPushed.transform.parent.gameObject;
                    childBone = objectPushed.transform.transform.GetChild(0).gameObject;
                    Debug.Log(parentBone + " " + childBone);
                    ApplyEffectRight(parentBone);
                    ApplyEffectRight(childBone);
                }
                else
                {
                    parentBone = null;
                    childBone = null;
                }

                ApplyEffectRight(objectPushed);
                objectPushed = null;

            }


        }
        else if (this.transform.gameObject.name == "Left")
        {
            if (objectPushed && objectPushed.GetComponent<Rigidbody>())
            {
                if (objectPushed.transform.parent.GetComponentInParent<EnemyScript>())
                {
                    parentBone = objectPushed.transform.parent.gameObject;
                    childBone = objectPushed.transform.transform.GetChild(0).gameObject;
                    Debug.Log(parentBone + " " + childBone);

                    ApplyEffectLeft(parentBone);
                    ApplyEffectLeft(childBone);
                }
                else
                {
                    parentBone = null;
                    childBone = null;
                }

                ApplyEffectLeft(objectPushed);
                objectPushed = null;

            }
        }
    }

    public void ApplyEffectRight(GameObject gameobject)
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            gameobject.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetVelocity() * pushFactor;
            gameobject.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetAngularVelocity() * pushFactor;
            gameobject.GetComponent<Rigidbody>().maxAngularVelocity = objectPushed.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
        }
        gameobject = null;
    }

    public void ApplyEffectLeft(GameObject gameobject)
    {
        if (gameObject.GetComponent<Rigidbody>())
        {
            gameobject.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetVelocity() * pushFactor;
            gameobject.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetAngularVelocity() * pushFactor;
            gameobject.GetComponent<Rigidbody>().maxAngularVelocity = objectPushed.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
        }
        gameobject = null;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && (collision.gameObject.GetComponent<Items>() || collision.gameObject.GetComponentInParent<EnemyScript>()))
        {
            objectPushed = collision.gameObject;
        }
    }

}
