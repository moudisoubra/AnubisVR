using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkingCollision : MonoBehaviour
{
    public bool collidingSoulSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "SoulSkill")
        {
            collidingSoulSkill = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SoulSkill")
        {
            collidingSoulSkill = false;
        }
    }
}
