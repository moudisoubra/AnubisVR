using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollTesting : MonoBehaviour
{
    public Collider MainCol;
    public Collider[] AllCol;
    public bool isRagoll;
    // Start is called before the first frame update


    private void Start()
    {
       
        AllCol = GetComponentsInChildren<Collider>(true);
        

        //USE IT WHEN HE DIE TO BECOME TRUE
        RagDoll(false);


    }

   
    public void RagDoll(bool isRagDoll)
    {
        GetComponent<Animator>().enabled = !isRagDoll;
        foreach (var col in AllCol)
        {
            col.enabled = isRagDoll;
            col.gameObject.GetComponent<Collider>().isTrigger = !isRagDoll;
            col.gameObject.GetComponent<Rigidbody>().isKinematic = !isRagDoll; 
            Debug.Log("Collider...");
            
            
        }

        GetComponent<Collider>().isTrigger = isRagDoll;
       GetComponent<Rigidbody>().isKinematic = isRagDoll;
        AllCol[0].enabled = !isRagDoll;

    }

    
}
