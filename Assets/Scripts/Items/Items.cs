using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public bool objectHeld;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (objectHeld)
        //{
        //    Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);

        //    foreach (Collider col in colliders)
        //    {
        //        if (col.gameObject.tag == "Hand" && !col.gameObject.GetComponent<Hands>().objectHeld && col.gameObject.GetComponent<Hands>().grabDown)
        //        {
        //            transform.parent = col.gameObject.transform;
        //        }
                
        //    }
        //}
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 0.1f);
    //}
}
