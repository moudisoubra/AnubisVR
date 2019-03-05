using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour
{

    Node root;

    // Start is called before the first frame update
    void Start()
    {
        root = new Selector();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
