using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {

    public BaseState CurrentState;

    public Transform[] Waypoints;
    public Transform WpParent;
    public Transform playerTransform;

    
    

    public GameObject Effect;
    public GameObject HoleEffect;

    public Transform eye;
    public float Damage;
    public float weaponRange;

    public float Health;
    // Use this for initialization
    void Start () {

        CurrentState = new EnemiesMove() ;

        Debug.Log("Moving");

	}
	
	// Update is called once per frame
	void Update () {
        CurrentState.UpdateState(this);


    }









}
