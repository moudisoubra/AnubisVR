using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public LineRenderer LR;

    // Start is called before the first frame update
    void Start()
    {
        LR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetLine(GameObject origin, GameObject target)
    {
        LR.SetPosition(0, origin.transform.position);

        if (target.gameObject == null)
        {
            Debug.Log("TARGET IS NULL");
            LR.SetPosition(1, transform.forward * 1000);
        }
        else
        {
            LR.SetPosition(1, target.transform.position);
        }
    }
}
