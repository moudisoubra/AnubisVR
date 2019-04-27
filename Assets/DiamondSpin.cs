using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondSpin : MonoBehaviour
{
    public bool spin;
    public float rotationSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            this.transform.Rotate(0, 0, rotationSpeed);
        }
    }
}
