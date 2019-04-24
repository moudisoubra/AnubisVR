using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    public LineRenderer lr;

    public bool activate;
    public bool permanent;

    public GameObject mirrorHit;
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }


    void Update()
    {
        if (activate || permanent)
        {
            lr.enabled = true;
            ActivateLaser();
        }
        else
        {
            lr.enabled = false;
        }
    }

    public void ActivateLaser()
    {
        lr.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.tag == "Mirror")
            {
                lr.SetPosition(1, hit.point);
                mirrorHit = hit.collider.gameObject;
                mirrorHit.GetComponent<Laser>().enabled = true;
                mirrorHit.GetComponent<Laser>().activate = true;
            }
            if ((hit.collider.tag != "Mirror" || hit.collider.gameObject == null) && mirrorHit)
            {
                mirrorHit.GetComponent<Laser>().activate = false;
            }
            if (hit.collider)
            {
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward * 100);
    }
}
