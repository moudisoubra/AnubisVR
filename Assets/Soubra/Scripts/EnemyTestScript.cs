using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    Animator enemyAnimator;
    public bool parryed;

    public GameObject collisionDetector;
    public BoxCollider axeCollider;


    void Start()
    {
        enemyAnimator = gameObject.GetComponent<Animator>();
        collisionDetector = GetComponentInChildren<DetectCollision>().gameObject;
    }
    
    void Update()
    {
        axeCollider = collisionDetector.GetComponent<BoxCollider>();
        parryed = GetComponentInChildren<DetectCollision>().collided;
        enemyAnimator.SetBool("Parry", parryed);
    }

    public void TurnTriggerOn()
    {
        axeCollider.enabled = true;
    }

    public void TurnTriggerOff()
    {
        axeCollider.enabled = false;
    }

    public void ReturnToNormal()
    {
        GetComponentInChildren<DetectCollision>().collided = false;
    }
}
