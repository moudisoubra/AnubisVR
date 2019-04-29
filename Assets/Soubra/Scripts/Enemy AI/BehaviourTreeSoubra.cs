using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSoubra : MonoBehaviour
{

    NodeSoubra root;
    NodeSoubra attackSequence;

    public GameObject selfObject;
    public Transform startPoint , eyes;
    public Transform lastPoint;
    public float speed = 10f;
    public float MaxDis = 5f;
    public float MaxVel = 5f;
    public float RangeRayCAST;
    public bool fail;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        selfObject = this.gameObject;
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
        root.nodesList.Add(new SoubraPatrol());

        attackSequence.nodesList.Add(new SoubraChase());
        attackSequence.nodesList.Add(new SoubraAttack());
    }






    public void Move(Vector3 dir)
    {
        rb.AddForce(dir);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxVel);
    }
}

