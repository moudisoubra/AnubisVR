using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AddForceToItems : MonoBehaviour
{
    public float pushFactor;

    public GameObject objectPushed;
    public GameObject mainParent;
    public GameObject parentBone;
    public GameObject childBone;
    public GameObject tempObject;

    public List<GameObject> enemyBones;
    public List<GameObject> hitEnemyBones;

    public AvatarMask enemyMask;
    public Animator animatorEnemy;
    public AnimatorOverrideController animController;

    public bool forceAttack;

    // Start is called before the first frame update
    void Start()
    {
        forceAttack = false;
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
                        mainParent = objectPushed.transform.parent.GetComponentInParent<EnemyScript>().gameObject;
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
            else if (this.transform.parent.GetComponentInParent<BoxCollider>().gameObject.name == "Left")
            {
                if (objectPushed && objectPushed.GetComponent<Rigidbody>())
                {
                    if (objectPushed.transform.parent && objectPushed.transform.parent.GetComponentInParent<EnemyScript>())
                    {
                        mainParent = objectPushed.transform.parent.GetComponentInParent<EnemyScript>().gameObject;
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
                    
                    FindEnemyBones();

                    ApplyEffectRight(parentBone);
                    ApplyEffectRight(childBone);
                }
                else
                {
                    parentBone = null;
                    childBone = null;
                }

                if (gameObject.GetComponent<Rigidbody>())
                {
                    objectPushed.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetAngularVelocity() * pushFactor;
                    objectPushed.GetComponent<Rigidbody>().maxAngularVelocity = objectPushed.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
                }
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

    public void ApplyEffectRight(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetVelocity() * pushFactor;
            obj.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().rightPose.GetAngularVelocity() * pushFactor;
            obj.GetComponent<Rigidbody>().maxAngularVelocity = obj.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
        }
        obj = null;
    }

    public void ApplyEffectLeft(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            obj.GetComponent<Rigidbody>().velocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetVelocity() * pushFactor;
            obj.GetComponent<Rigidbody>().angularVelocity = this.transform.parent.GetComponentInParent<AnubisController>().leftPose.GetAngularVelocity() * pushFactor;
            obj.GetComponent<Rigidbody>().maxAngularVelocity = obj.GetComponent<Rigidbody>().angularVelocity.magnitude * pushFactor;
        }
        obj = null;
    }

    public void FindEnemyBones()
    {
        enemyBones.Clear();
        hitEnemyBones.Clear();

        mainParent = objectPushed.transform.parent.GetComponentInParent<EnemyScript>().gameObject;


        parentBone = objectPushed.transform.parent.gameObject;
        childBone = objectPushed.transform.transform.GetChild(0).gameObject;

        AddAllChildren(mainParent.transform, enemyBones, hitEnemyBones);
        CreateMask();
    }

    private void AddAllChildren(Transform parent, List<GameObject> fullList, List<GameObject> hitList)
    {
        
        foreach (Transform child in parent)
        {

            if (child.gameObject.GetComponent<Rigidbody>() && (child.gameObject == parentBone || child.gameObject == childBone || child.gameObject == objectPushed))
            {
                hitList.Add(child.gameObject);
            }

            fullList.Add(child.gameObject);

            AddAllChildren(child, fullList, hitList);
        }
    }

    public void CreateMask()
    {
        enemyMask = new AvatarMask();
        enemyMask.AddTransformPath(mainParent.transform);

        for (int i = 1; i < enemyBones.Count; i++)
        {
            if (enemyBones[i].gameObject.name == parentBone.name || enemyBones[i].gameObject.name == childBone.name || enemyBones[i].gameObject.name == objectPushed.name)
            {
                enemyMask.SetTransformActive(i, false);
            }

            else 
                enemyMask.SetTransformActive(i, true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && (collision.gameObject.GetComponent<Items>() || collision.gameObject.GetComponentInParent<EnemyScript>()))
        {
            objectPushed = collision.gameObject;
        }

        //if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponentInParent<EnemyScript>() && collision.gameObject.tag == "Real")
        //{
        //    forceAttack = true;
        //    var scriptLink = collision.gameObject.GetComponentInParent<TestBodyParts>();
        //    scriptLink.ClearAll();
        //    Debug.Log("CLEAR ALL");
        //}
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponentInParent<EnemyScript>())
        {
            forceAttack = true;
            var scriptLink = collision.gameObject.GetComponentInParent<TestBodyParts>();
            scriptLink.ClearAll();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponentInParent<EnemyScript>()
        && forceAttack)
        {
            forceAttack = false;
            var scriptLink = collision.gameObject.GetComponentInParent<TestBodyParts>();
            scriptLink.AddAllChildren(collision.gameObject.transform, scriptLink.actualBody);
            scriptLink.AddAllTransforms(collision.gameObject.transform, scriptLink.actualBodyTransforms);

            for (int i = 0; i < scriptLink.fullRigInvisi.Count; i++)
            {
                if (scriptLink.fullRigInvisi[i].gameObject.name == collision.gameObject.name)
                {
                    Debug.Log("Tempobject found: " + tempObject);
                    tempObject = scriptLink.fullRigInvisi[i].gameObject;
                }
            }

            if (tempObject != null && tempObject.name == collision.gameObject.name)
            {
                Debug.Log("using tempObject: " + tempObject);
                scriptLink.AddAllChildren(tempObject.transform, scriptLink.invisibleBody);
                scriptLink.AddAllTransforms(tempObject.transform, scriptLink.invisiBodyTransforms);
            }

        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<Rigidbody>() && collision.gameObject.GetComponentInParent<EnemyScript>() 
    //        && forceAttack && collision.gameObject.tag == "Real")
    //    {
    //        forceAttack = false;
    //        var scriptLink = collision.gameObject.GetComponentInParent<TestBodyParts>();
    //        scriptLink.AddAllChildren(collision.gameObject.transform, scriptLink.actualBody);
    //        Debug.Log("HI");
    //        scriptLink.AddAllTransforms(collision.gameObject.transform, scriptLink.actualBodyTransforms);

    //        for (int i = 0; i < scriptLink.fullRigInvisi.Count; i++)
    //        {
    //            if (scriptLink.fullRigInvisi[i].gameObject.name == collision.gameObject.name)
    //            {
    //                Debug.Log("Tempobject found: " + tempObject);
    //                tempObject = scriptLink.fullRigInvisi[i].gameObject;
    //            }
    //        }

    //        if (tempObject != null && tempObject.name == collision.gameObject.name)
    //        {
    //            Debug.Log("using tempObject: " + tempObject);
    //            scriptLink.AddAllChildren(tempObject.transform, scriptLink.invisibleBody);
    //            scriptLink.AddAllTransforms(tempObject.transform, scriptLink.invisiBodyTransforms);
    //        }

    //    }
    //}

}
