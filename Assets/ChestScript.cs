using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    public GameObject chestLid;
    public GameObject handCollided;
    public float rotationSpeed;
    public float distanceMoved;
    public bool anubisCollided;
    public bool moveChest;
    public Vector3 firstPosition;
    public Vector3 secondPosition;
    public AnubisController anubisScript;

    // Start is called before the first frame update
    void Start()
    {
        anubisScript = FindObjectOfType<AnubisController>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenChest();

        if (moveChest)
        {
            //distanceMoved = distanceMoved * -100;
            distanceMoved = Vector3.Distance(firstPosition, secondPosition) * -0.1f;
            Debug.Log(secondPosition + "DISTANCE" + distanceMoved);
            chestLid.transform.Rotate(distanceMoved * 100, 0, 0);
        }
    }

    public void OpenChest()
    {
        if (anubisCollided)
        {
            if (handCollided.GetComponentInParent<AnubisController>().rightGrab || handCollided.GetComponentInParent<AnubisController>().leftGrab)
            {
                secondPosition = handCollided.transform.position;
                moveChest = true;
            }
            else
            {
                moveChest = false;
                firstPosition = handCollided.transform.position;
                distanceMoved = 0;
                //Debug.Log(firstPosition);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
            handCollided = other.gameObject;
            anubisCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Hand")
        {
        }
    }
}
