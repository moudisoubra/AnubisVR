using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceLever : MonoBehaviour
{
    public GameObject lever;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == lever)
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = Vector3.Lerp(other.transform.position, this.transform.position, 1);
            Destroy(other.gameObject);
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
