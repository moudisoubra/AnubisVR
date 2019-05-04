using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSoubra : MonoBehaviour
{

    NodeSoubra root;
    NodeSoubra attackSequence;
    public Rigidbody rb;
    public GameObject selfObject;
    public Transform startPoint;
    public Transform lastPoint;
    public float distanceToChase;
    public float speed = 10f;
    public float MaxDis = 5f;
    public bool fail;
    public bool chaseFail;

    void Start()
    {
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
        root.nodesList.Add(attackSequence);
        root.nodesList.Add(new SoubraPatrolOriginal());

        attackSequence.nodesList.Add(new SoubraChaseOriginal());
        attackSequence.nodesList.Add(new SoubraAttack());
    }
}
