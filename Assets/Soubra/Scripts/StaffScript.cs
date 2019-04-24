using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffScript : MonoBehaviour
{
    public GameObject rotatingEye;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent)
        {
            rotatingEye.transform.Rotate(0, 0, rotationSpeed);
        }
        else
        {
            rotatingEye.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
