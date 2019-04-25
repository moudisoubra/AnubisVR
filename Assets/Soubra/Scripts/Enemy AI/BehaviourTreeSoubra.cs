using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeSoubra : MonoBehaviour
{

    NodeSoubra root;
    NodeSoubra attackSequence;

    public GameObject selfObject;
    public Transform startPoint;
    public Transform lastPoint;
    public float speed = 10f;
    public float MaxDis = 5f;
    public bool fail;

    void Start()
    {
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
}
