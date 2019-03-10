using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSoubra : MonoBehaviour
{

    NodeSoubra root;

    // Start is called before the first frame update
    void Start()
    {
        root = new SelectorSoubra();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
