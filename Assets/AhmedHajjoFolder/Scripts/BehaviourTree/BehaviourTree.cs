using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree : MonoBehaviour {
    
    public bool fly;
    public float time;
    public float speed;

    Node root;

	void Start ()
    {
        fly = true;
        root = new Selector();
        root.childrenNodes.Add(new Fly());
        root.childrenNodes.Add(new Fall());
	}
	
	void Update ()
    {
        time += Time.deltaTime;

        root.Execute(this);
	}
}
