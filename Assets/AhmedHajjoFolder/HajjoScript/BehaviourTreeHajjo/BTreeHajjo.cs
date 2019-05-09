using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTreeHajjo : MonoBehaviour
{
    NodesHajjo root;
    NodesHajjo AttSeq;

    public Transform startPoint;
    public Transform lastPoint;

    public GameObject selfObject;

    public GameObject playerHead;
    public Animator animator;
    public Rigidbody rb;

    public int Health = 100;
    public int Mana = 100;
    
    public float Timer;
    public float chase = 5f;


    public float MaxSpeed;
    public float maxVel;

    // Start is called before the first frame update
    void Start()
    {
        root = new SelectorHajjo();
        AttSeq = new SequencesHajjo();
        AddChildren();
        selfObject = this.gameObject;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        root.Execute(this);
    }


    void AddChildren()
    {
        root.ListOfNodes.Add(new HAJJODeath());
        root.ListOfNodes.Add(AttSeq);
        root.ListOfNodes.Add(new HajjoPatrol());

        AttSeq.ListOfNodes.Add(new HajjoChase());
        AttSeq.ListOfNodes.Add(new AttackHajjo());
    }


    public void Move(Vector3 dir)
    {
        rb.AddForce(dir * MaxSpeed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVel);
    }






    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chase);
    }

}
