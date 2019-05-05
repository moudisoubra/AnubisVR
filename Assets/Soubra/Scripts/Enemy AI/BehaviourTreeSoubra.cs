using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSoubra : MonoBehaviour
{

    NodeSoubra root;
    NodeSoubra attackSequence;
    public GameObject playerHead;
    public Animator animator;
    public Rigidbody rb;
    public GameObject selfObject;
    public Transform startPoint;
    public Transform lastPoint;
    public int hitPoints;
    public int maxHitPoints;
    public float distanceToChase;
    public float speed = 10f;
    public float MaxDis = 5f;
    public float waitTimer = 0;
    public bool fail;
    public bool chaseFail;

    void Start()
    {
        animator = GetComponent<Animator>();
        selfObject = this.gameObject;
        rb = GetComponent<Rigidbody>();
        root = new SelectorSoubra();
        attackSequence = new SequenceSoubra();
        AddChildren();
    }


    void Update()
    {
        root.Execute(this);
    }

    void AddChildren()
    {
        root.nodesList.Add(new SoubraDieOriginal());
        root.nodesList.Add(attackSequence);
        root.nodesList.Add(new SoubraPatrolOriginal());

        attackSequence.nodesList.Add(new SoubraChaseOriginal());
        attackSequence.nodesList.Add(new SoubraAttack());
    }
}
