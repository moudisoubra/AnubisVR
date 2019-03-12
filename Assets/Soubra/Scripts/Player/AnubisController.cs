using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AnubisController : MonoBehaviour
{
    public SteamVR_Behaviour_Pose rightPose;
    public SteamVR_Behaviour_Pose leftPose;

    public SteamVR_Action_Single rightSqueeze;
    public SteamVR_Action_Single leftSqueeze;

    public SteamVR_Action_Vector2 rightTouchpad;
    public SteamVR_Action_Vector2 leftTouchpad;

    public SteamVR_Input_Sources rightController;
    public SteamVR_Input_Sources leftController;

    public float rightTriggerValue;
    public float leftTriggerValue;
    public float playerSpeed;
    public float soulSkillTimer;
    public float necromancyTimer;

    public float rightControllerPosition;
    public float leftControllerPosition;
    public float rightControllerCurrent;
    public float leftControllerCurrent;


    public int flyingValue;
    public int resetNumber;

    public Vector2 rightTouchpadValue;
    public Vector2 leftTouchpadValue;

    public Vector3 rightControllerVelocity;
    public Vector3 leftControllerVelocity;
    public Vector3 previousPosition;
    public Vector3 previousPlayerPosition;
    public Vector3 rightVelocity;
    public Vector3 leftVelocity;

    public Transform enemyTarget;

    public bool rightTeleport;
    public bool leftTeleport;
    public bool rightGrab;
    public bool leftGrab;
    public bool rightGrabUp;
    public bool leftGrabUp;
    public bool rightGrabDown;
    public bool leftGrabDown;
    public bool rightGrip;
    public bool leftGrip;
    public bool throwObject;
    public bool soulSkill;
    public bool rightSoulCheck;
    public bool leftSoulCheck;
    public bool necromancyCheck;
    public bool commandMinions;

    public GameObject rightObjectHeld;
    public GameObject leftObjectHeld;
    public GameObject previousParent;
    public GameObject rightHandModel;
    public GameObject leftHandModel;
    public GameObject fullRig;
    public GameObject rightModel;
    public GameObject leftModel;
    public GameObject anubisPlaceHolder;
    public GameObject anubis;
    public GameObject sandTrail;
    public GameObject currentTrail;

    public List<GameObject> controlledObjects = new List<GameObject>();

    public Material normalMaterial;
    public Material ghostMaterial;
    public Material minionMaterial;

    public Animator rightHandAnimator;
    public Animator leftHandAnimator;
    public Camera playerCamera;

    public checkingCollision rightChecker;
    public checkingCollision leftChecker;
    public DrawLine drScript;
    public LayerMask enemyLayer;
    //public LineRenderer lineRenderer;

    public AddForceToItems RightAddForceScript;
    public AddForceToItems LeftAddForceScript;

    void Start()
    {
        RightAddForceScript = rightModel.GetComponent<AddForceToItems>();
        LeftAddForceScript = leftModel.GetComponent<AddForceToItems>();
          //drScript = FindObjectOfType<DrawLine>();
          //drScript.gameObject.SetActive(false);
          rightChecker = rightModel.GetComponentInParent<checkingCollision>();
        leftChecker = leftModel.GetComponentInParent<checkingCollision>();
        rightPose = rightModel.GetComponent<SteamVR_Behaviour_Pose>();
        leftPose = leftModel.GetComponent<SteamVR_Behaviour_Pose>();
        soulSkill = false;
        commandMinions = false;
        soulSkillTimer = 0;
        necromancyTimer = 10;
        resetNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightGrab && !rightObjectHeld)
        {
            rightModel.GetComponent<BoxCollider>().isTrigger = false;
            RightAddForceScript.enabled = true;
        }
        else
        {
            rightModel.GetComponent<BoxCollider>().isTrigger = true;
            RightAddForceScript.enabled = false;
        }

        if (leftGrab && !leftObjectHeld)
        {
            leftModel.GetComponent<BoxCollider>().isTrigger = false;
            LeftAddForceScript.enabled = true;
        }
        else
        {
            leftModel.GetComponent<BoxCollider>().isTrigger = true;
            LeftAddForceScript.enabled = false;
        }

        rightVelocity = rightPose.GetVelocity();
        leftVelocity = leftPose.GetVelocity();

        necromancyTimer += Time.deltaTime;

        //lineRenderer.SetPosition(0, rightModel.transform.position);
        //lineRenderer.SetPosition(1, rightModel.transform.forward * 1000);

        if (necromancyTimer > 10)
        {
            necromancyCheck = true;
        }
        else
        {
            //controlledObjects.Clear();
            necromancyCheck = false;
            this.rightHandAnimator.SetBool("RaisingDead", false);
            this.leftHandAnimator.SetBool("RaisingDead", false);
        }

        if (commandMinions && controlledObjects.Count != 0)
        {
            //drScript.gameObject.SetActive(true);
            if (currentTrail)
            {
                currentTrail.transform.position = Vector3.MoveTowards(currentTrail.transform.position, enemyTarget.position, 0.1f);
            }
            for (int i = 0; i < controlledObjects.Count; i++)
            {
                controlledObjects[i].transform.position = Vector3.MoveTowards(controlledObjects[i].transform.position, enemyTarget.position, 0.05f);
            }
        }

        rightSoulCheck = rightChecker.collidingSoulSkill;
        leftSoulCheck = leftChecker.collidingSoulSkill;
        soulSkillTimer += Time.deltaTime;

        rightGrab = SteamVR_Actions._default.GrabPinch.GetState(rightController);
        leftGrab = SteamVR_Actions._default.GrabPinch.GetState(leftController);
        rightGrabUp = SteamVR_Actions._default.GrabPinch.GetStateUp(rightController);
        leftGrabUp = SteamVR_Actions._default.GrabPinch.GetStateUp(leftController);
        rightGrabDown = SteamVR_Actions._default.GrabPinch.GetStateDown(rightController);
        leftGrabDown = SteamVR_Actions._default.GrabPinch.GetStateDown(leftController);
        rightTeleport = SteamVR_Actions._default.Teleport.GetState(rightController);
        leftTeleport = SteamVR_Actions._default.Teleport.GetState(leftController);
        rightGrip = SteamVR_Actions._default.GrabGrip.GetState(rightController);
        leftGrip = SteamVR_Actions._default.GrabGrip.GetState(leftController);

        rightTriggerValue = rightSqueeze.GetAxis(rightController);
        leftTriggerValue = rightSqueeze.GetAxis(leftController);

        rightTouchpadValue = rightTouchpad.GetAxis(rightController);
        leftTouchpadValue = leftTouchpad.GetAxis(leftController);

        //if (SteamVR_Actions._default.Teleport.GetState(controller))
        //{
        //    Debug.Log("Touchpad Value " + touchpadValue);
        //}

        AnimateHand();
        GrabObjects();
        MovePlayer();
        SoulSkill();
        ResetPlayerPosition();
        NecromancySkill();
        pointAtTarget();
    }

    public void MovePlayer()
    {
        Debug.Log(rightTouchpadValue);
        if (rightTeleport && rightTouchpadValue.x <= 0.5 && rightTouchpadValue.y >= 0.5)
        {//Right Controller
            fullRig.transform.position += new Vector3(rightModel.transform.forward.x,
                rightModel.transform.forward.y * flyingValue,
                rightModel.transform.forward.z) * playerSpeed * Time.deltaTime;
        }
        if (rightTeleport && rightTouchpadValue.x <= 0.5 && rightTouchpadValue.y <= 0.5)
        {
            fullRig.transform.position -= new Vector3(rightModel.transform.forward.x,
            rightModel.transform.forward.y * flyingValue,
            rightModel.transform.forward.z) * playerSpeed * Time.deltaTime;
        }
    }

    public void AnimateHand()
    {
        if (rightGrabDown)
        {
            this.rightHandAnimator.SetBool("Grabbing", true);

        }
        else if (rightGrabUp)
        {
            this.rightHandAnimator.SetBool("Grabbing", false);
        }

        if (leftGrabDown)
        {
            this.leftHandAnimator.SetBool("Grabbing", true);
        }
        else if (leftGrabUp)
        {
            this.leftHandAnimator.SetBool("Grabbing", false);
        }
        if (rightTeleport && rightTouchpadValue.x >= 0.5 && rightTouchpadValue.y <= 0.5)
        {
            this.rightHandAnimator.SetBool("Pointing", true);
        }
        else
        {
            this.rightHandAnimator.SetBool("Pointing", false);
        }
    }

    public void GrabObjects()
    {

        if (rightGrabDown)
        {
            Collider[] colliders = Physics.OverlapSphere(rightModel.transform.position - new Vector3(0, 0.05f, 0), 0.05f);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.GetComponent<Items>())
                {
                    rightObjectHeld = col.gameObject;
                    rightObjectHeld.transform.parent = rightHandModel.transform;
                    rightObjectHeld.GetComponent<Rigidbody>().isKinematic = true;
                    col.gameObject.GetComponent<Items>().objectHeld = true;
                    break;
                }

                if (col.gameObject.tag == "Interactable")
                {
                    rightObjectHeld = col.gameObject;
                    rightObjectHeld.GetComponent<Rigidbody>().useGravity = false;
                    previousParent = rightObjectHeld.transform.parent.gameObject;
                    rightObjectHeld.transform.parent = rightHandModel.transform;
                }
            }
        }
        if (leftGrabDown)
        {
            Collider[] colliders = Physics.OverlapSphere(leftModel.transform.position - new Vector3(0, 0.05f, 0), 0.05f);

            foreach (Collider col in colliders)
            {
                if (col.gameObject.GetComponent<Items>())
                {
                    leftObjectHeld = col.gameObject;
                    leftObjectHeld.transform.parent = leftHandModel.transform;
                    leftObjectHeld.GetComponent<Rigidbody>().isKinematic = true;
                    col.gameObject.GetComponent<Items>().objectHeld = true;
                    break;
                }

                if (col.gameObject.tag == "Interactable")
                {
                    leftObjectHeld = col.gameObject;
                    leftObjectHeld.GetComponent<Rigidbody>().useGravity = false;
                    previousParent = leftObjectHeld.transform.parent.gameObject;
                    leftObjectHeld.transform.parent = rightHandModel.transform;
                }
            }
        }

        if (rightGrabUp && rightObjectHeld && rightObjectHeld.transform.parent == rightHandModel.transform)
        {
            if (rightObjectHeld.tag == "Interactable")
            {
                rightObjectHeld.GetComponent<Rigidbody>().useGravity = true;
                rightObjectHeld.transform.parent = previousParent.transform;
                previousParent = null;
            }
            else
            {
                RightThrowObjects();
                rightObjectHeld.transform.parent = null;
                rightObjectHeld.GetComponent<Rigidbody>().isKinematic = false;
                rightObjectHeld.GetComponent<Items>().objectHeld = false;
                rightObjectHeld = null;
            }
        }

        if (leftGrabUp && leftObjectHeld && leftObjectHeld.transform.parent == leftHandModel.transform)
        {
            if (leftObjectHeld.tag == "Interactable")
            {
                leftObjectHeld.GetComponent<Rigidbody>().useGravity = true;
                leftObjectHeld.transform.parent = previousParent.transform;
                previousParent = null;
            }
            else
            {
                LeftThrowObjects();
                leftObjectHeld.transform.parent = null;
                leftObjectHeld.GetComponent<Rigidbody>().isKinematic = false;
                leftObjectHeld.GetComponent<Items>().objectHeld = false;
                leftObjectHeld = null;
            }
        }
    }

    public void RightThrowObjects()
    {
        rightObjectHeld.GetComponent<Rigidbody>().velocity = rightPose.GetVelocity();
        rightObjectHeld.GetComponent<Rigidbody>().angularVelocity = rightPose.GetAngularVelocity();

        rightObjectHeld.GetComponent<Rigidbody>().maxAngularVelocity = rightObjectHeld.GetComponent<Rigidbody>().angularVelocity.magnitude;
    }

    public void LeftThrowObjects()
    {
        leftObjectHeld.GetComponent<Rigidbody>().velocity = leftPose.GetVelocity();
        leftObjectHeld.GetComponent<Rigidbody>().angularVelocity = leftPose.GetAngularVelocity();

        leftObjectHeld.GetComponent<Rigidbody>().maxAngularVelocity = leftObjectHeld.GetComponent<Rigidbody>().angularVelocity.magnitude;
    }

    public void SoulSkill()
    {
        if (rightSoulCheck && leftSoulCheck && rightGrip && leftGrip && !soulSkill)
        {
            anubis = Instantiate(anubisPlaceHolder, transform.position, transform.rotation);
            soulSkill = true;
            soulSkillTimer = 0;
            previousPlayerPosition = transform.position;
        }

        if (soulSkillTimer < 11 && soulSkill)
        {
            rightHandModel.gameObject.GetComponent<Renderer>().material = ghostMaterial;
            leftHandModel.gameObject.GetComponent<Renderer>().material = ghostMaterial;
            if (fullRig.transform.position.y < 0)
            {
                fullRig.transform.position = new Vector3(fullRig.transform.position.x, 0, fullRig.transform.position.z);
            }
            flyingValue = 1;
            resetNumber = 0;
        }
        else
        {
            rightHandModel.gameObject.GetComponent<Renderer>().material = normalMaterial;
            leftHandModel.gameObject.GetComponent<Renderer>().material = normalMaterial;
            flyingValue = 0;
            soulSkill = false;
        }

    }

    public void NecromancySkill()
    {

        if (!rightGrip && !leftGrip)
        {
            rightControllerPosition = rightModel.transform.position.y;
            leftControllerPosition = leftModel.transform.position.y;
        }

        if (rightGrip && leftGrip)
        {
            rightControllerCurrent = rightModel.transform.position.y;
            leftControllerCurrent = leftModel.transform.position.y;

            if ((rightControllerCurrent > rightControllerPosition) && (leftControllerCurrent > leftControllerPosition) && necromancyCheck)
            {
                necromancyTimer = 0;
                this.rightHandAnimator.SetBool("RaisingDead", true);
                this.leftHandAnimator.SetBool("RaisingDead", true);
                Collider[] colliders = Physics.OverlapSphere(playerCamera.transform.position, 3);
                
                foreach (Collider col in colliders)
                {
                    if (col.gameObject.tag == "Enemy" && controlledObjects.Count < 5)
                    {
                        controlledObjects.Add(col.gameObject);

                        for (int i = 0; i < controlledObjects.Count; i++)
                        {
                            controlledObjects[i].GetComponent<Renderer>().material = minionMaterial;
                            //controlledObjects[i].gameObject.GetComponent<Rigidbody>().useGravity = false;
                            //controlledObjects[i].gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 10);
                        }
                    }
                }
            }
        }


    }

    public void pointAtTarget()
    {
        //drScript.GetLine(rightModel, null);

        if (rightTeleport && rightTouchpadValue.x >= 0.5 && rightTouchpadValue.y <= 0.5)
        {
            RaycastHit hit;

                Debug.DrawRay(rightModel.transform.position, rightModel.transform.TransformDirection(Vector3.forward), Color.yellow);

            if (Physics.Raycast(rightModel.transform.position, rightModel.transform.forward, out hit, Mathf.Infinity, enemyLayer))
            {
                if (hit.transform.gameObject.GetComponent<EnemyTestScript>())
                {
                    //drScript.gameObject.SetActive(true);
                    //drScript.GetLine(rightModel, hit.transform.gameObject);
                    currentTrail = Instantiate(sandTrail, rightModel.transform.position, Quaternion.identity);
                    enemyTarget = hit.transform;
                    commandMinions = true;
                }
            }
        }

        if (leftTeleport && leftTouchpadValue.x >= 0.5 && leftTouchpadValue.y <= 0.5)
        {
            RaycastHit hit;

            Debug.DrawRay(leftModel.transform.position, leftModel.transform.TransformDirection(Vector3.forward), Color.yellow);

            if (Physics.Raycast(leftModel.transform.position, leftModel.transform.forward, out hit, Mathf.Infinity, enemyLayer))
            {
                if (hit.transform.gameObject.GetComponent<EnemyTestScript>())
                {
                    //drScript.gameObject.SetActive(true);
                    //drScript.GetLine(rightModel, hit.transform.gameObject);
                    currentTrail = Instantiate(sandTrail, leftModel.transform.position, Quaternion.identity);
                    enemyTarget = hit.transform;
                    commandMinions = true;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(playerCamera.transform.position, 3);
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
