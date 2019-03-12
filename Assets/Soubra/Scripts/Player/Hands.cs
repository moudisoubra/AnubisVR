using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class Hands : MonoBehaviour
{

    public SteamVR_Behaviour_Pose pose;
    public SteamVR_Action_Single squeezeAction;
    public SteamVR_Action_Vector2 touchpadAction;

    public SteamVR_Input_Sources controller;

    public float triggerValue;
    public float playerSpeed;
    public float soulSkillTimer;

    public int flyingValue;
    public int resetNumber;

    public Vector2 touchpadValue;

    public Vector3 controllerVelocity;
    public Vector3 previousPosition;
    public Vector3 previousPlayerPosition;

    public bool moving;
    public bool grabUp;
    public bool grabDown;
    public bool throwObject;
    public bool grip;
    public bool soulSkill;

    public GameObject handModel;
    public GameObject otherHandModel;
    public GameObject fullRig;
    public GameObject model;
    public GameObject oppositeModel;
    public GameObject objectHeld;
    public GameObject anubisPlaceHolder;
    public GameObject anubis;

    public Material ghostMaterial;
    public Material normalMaterial;

    public Animator handAnimator;

    public Camera playerCamera;

    void Start()
    {
        handAnimator = GetComponent<Animator>();
        pose = GetComponentInParent<SteamVR_Behaviour_Pose>();
        soulSkill = false;
        soulSkillTimer = 0;
        resetNumber = 1;
    }
    
    void Update()
    {
        soulSkillTimer += Time.deltaTime;
        grabUp = SteamVR_Actions._default.GrabPinch.GetStateUp(controller);
        grabDown = SteamVR_Actions._default.GrabPinch.GetStateDown(controller);
        moving = SteamVR_Actions._default.Teleport.GetState(controller);
        grip = SteamVR_Actions._default.GrabGrip.GetState(controller);

        triggerValue = squeezeAction.GetAxis(controller);
        touchpadValue = touchpadAction.GetAxis(controller);

        //if (SteamVR_Actions._default.Teleport.GetState(controller))
        //{
        //    Debug.Log("Touchpad Value " + touchpadValue);
        //}

        AnimateHand();
        GrabObjects();
        MovePlayer();
        SoulSkill();
        ResetPlayerPosition();
    }

    public void MovePlayer()
    {
        if (moving && touchpadValue.x <= 0.5 && touchpadValue.y >= 0.5)
        {
            fullRig.transform.position += new Vector3(model.transform.forward.x,
                model.transform.forward.y * flyingValue,
                model.transform.forward.z) * playerSpeed * Time.deltaTime;
        }
    }


    public void AnimateHand()
    {
        if (grabDown)
        {
            this.handAnimator.SetBool("Grabbing", true);
        }
        else if (grabUp)
        {
            this.handAnimator.SetBool("Grabbing", false);
        }
    }

    public void GrabObjects()
    {
        if (objectHeld && objectHeld.transform.parent != this.gameObject.transform)
        {
            objectHeld = null;
        }

        if (!objectHeld && grabDown)
        {
                Collider[] colliders = Physics.OverlapSphere(model.transform.position - new Vector3(0, 0.05f, 0), 0.1f);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.GetComponent<Items>())
                {
                    objectHeld = col.gameObject;
                    objectHeld.transform.parent = transform;
                    objectHeld.GetComponent<Rigidbody>().isKinematic = true;
                    col.gameObject.GetComponent<Items>().objectHeld = true;
                }
            }
        }
        if (grabUp && objectHeld && objectHeld.transform.parent == this.gameObject.transform)
        {
            ThrowObjects();
            objectHeld.transform.parent = null;
            objectHeld.GetComponent<Rigidbody>().isKinematic = false;
            objectHeld.GetComponent<Items>().objectHeld = false;
            objectHeld = null;
        }
    }

    public void ThrowObjects()
    {
        objectHeld.GetComponent<Rigidbody>().velocity = pose.GetVelocity();
        objectHeld.GetComponent<Rigidbody>().angularVelocity = pose.GetAngularVelocity();

        objectHeld.GetComponent<Rigidbody>().maxAngularVelocity = objectHeld.GetComponent<Rigidbody>().angularVelocity.magnitude;

        
    }

    public void SoulSkill()
    {
        Collider[] colliders = Physics.OverlapSphere(model.transform.position - new Vector3(0, 0.05f, 0), 0.1f);

        foreach (Collider col in colliders)
        {
            if (col.gameObject.tag == "SoulSkill" && grip && !soulSkill)
            {
                anubis = Instantiate(anubisPlaceHolder, transform.position, transform.rotation);
                soulSkill = true;
                soulSkillTimer = 0;
                previousPlayerPosition = transform.position;
            }
        }

        if (soulSkillTimer < 11 && soulSkill)
        {
            handModel.gameObject.GetComponent<Renderer>().material = ghostMaterial;
            otherHandModel.gameObject.GetComponent<Renderer>().material = ghostMaterial;
            if (fullRig.transform.position.y < 0)
            {
                fullRig.transform.position = new Vector3(fullRig.transform.position.x, 0, fullRig.transform.position.z);
            }
            flyingValue = 1;
            resetNumber = 0;
        }
        else
        {
            handModel.gameObject.GetComponent<Renderer>().material = normalMaterial;
            otherHandModel.gameObject.GetComponent<Renderer>().material = normalMaterial;
            flyingValue = 0;
            soulSkill = false;
        }

    }

    public void ResetPlayerPosition()
    {
        if (!soulSkill && resetNumber == 0)
        {
            previousPlayerPosition.y = 0;
            fullRig.transform.position = previousPlayerPosition;
            resetNumber = 1;
            Destroy(anubis);
        }
    }
}
